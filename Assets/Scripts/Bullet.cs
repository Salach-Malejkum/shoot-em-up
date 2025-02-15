using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    [Header("Audio")]
    [SerializeField] private AudioClip explosion;

    private void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime * Vector2.up);

        if (!CheckIfOnScreen()) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.Instance.PlaySound(explosion);
        if (other.gameObject.TryGetComponent<Asteroid>(out Asteroid asteroid)) {
            asteroid.TakeDamage(tag);
        }
        Destroy(gameObject);
    }

    private bool CheckIfOnScreen()
    {
        if (transform.position.y > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y) {
            return false;
        }

        return true;
    }
}
