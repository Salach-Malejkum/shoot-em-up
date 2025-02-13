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

    private bool isPaused = false;

    private Vector2 moveInput;
    
    private void Update() {
        Move();
    }

    private void OnMove(InputValue input) {
        moveInput = input.Get<Vector2>();
    }

    private void OnShoot() {
        if (!isPaused) {
            Instantiate(bullet, spawnerTransform.position, Quaternion.identity);
            AudioManager.Instance.PlaySound(singleShoot);
        }
    }

    private void OnPause() {
        if (!isPaused) {
            mixer.SetFloat("LowpassFreq", 500);
            Time.timeScale = 0;
        } else {
            mixer.SetFloat("LowpassFreq", 22000);
            Time.timeScale = 1;
        }

        isPaused = !isPaused;
    }

    private void Move() {
        if (moveInput != Vector2.zero) {
            transform.Translate(moveInput * movementSpeed * Time.deltaTime);
        }

        Vector2 screenLimits = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 pos;
        pos.x = Mathf.Clamp(transform.position.x, -screenLimits.x, screenLimits.x);
        pos.y = Mathf.Clamp(transform.position.y, -screenLimits.y, screenLimits.y);

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        AudioManager.Instance.PlaySound(explosion);
        if (other.gameObject.TryGetComponent<Asteroid>(out Asteroid asteroid)) {
            asteroid.TakeDamage();
        }
        Destroy(gameObject);
    }
}
