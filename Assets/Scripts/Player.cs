using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnerTransform;
    
    [Header("Audio")]
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioClip singleShoot;
    [SerializeField] private AudioClip explosion;

    [Header("VisualDamageHandler")]
    [SerializeField] private SpriteRenderer sprite;
    private float blinkElapsedTime = 0;
    private bool isBlinking = false;

    private bool isPaused = false;

    private Vector2 moveInput;
    
    private void Update()
    {
        Move();
        RedBlink();
    }

    private void OnMove(InputValue input) {
        moveInput = input.Get<Vector2>();
    }

    private void OnShoot()
    {
        if (!isPaused) {
            Instantiate(bullet, spawnerTransform.position, Quaternion.identity);
            AudioManager.Instance.PlaySound(singleShoot);
        }
    }

    private void OnPause()
    {
        if (!isPaused) {
            mixer.SetFloat("LowpassFreq", 500);
            Time.timeScale = 0;
        } else {
            mixer.SetFloat("LowpassFreq", 22000);
            Time.timeScale = 1;
        }

        isPaused = !isPaused;
    }

    private void Move()
    {
        if (moveInput != Vector2.zero) {
            transform.Translate(moveInput * movementSpeed * Time.deltaTime);
        }

        Vector2 screenLimits = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 pos;
        pos.x = Mathf.Clamp(transform.position.x, -screenLimits.x, screenLimits.x);
        pos.y = Mathf.Clamp(transform.position.y, -screenLimits.y, screenLimits.y);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenLimits.x, screenLimits.x), Mathf.Clamp(transform.position.y, -screenLimits.y, screenLimits.y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.Instance.PlaySound(explosion);
        if (other.gameObject.TryGetComponent<Asteroid>(out Asteroid asteroid)) {
            asteroid.TakeDamage(tag);
        }
        TakeDamage();
    }

    private void RedBlink()
    {
        if (isBlinking && blinkElapsedTime < 1) {
            sprite.color = Mathf.Repeat(blinkElapsedTime, 0.2f) < 0.1f ? Color.red : Color.white;
            blinkElapsedTime += Time.deltaTime;
            print(blinkElapsedTime);
        } else if (isBlinking) {
            sprite.color = Color.white;
            blinkElapsedTime = 0;
            isBlinking = false;
        }
    }

    private void TakeDamage()
    {
        isBlinking = true;
        if (GameManager.Instance.PlayerTookDamage()) {
            Destroy(gameObject);
        }
    }
}
