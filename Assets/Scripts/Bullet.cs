using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;

    private void Update()
    {
        transform.Translate(bulletSpeed * Time.deltaTime * Vector2.up);

        if (!CheckIfOnScreen()) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }

    private bool CheckIfOnScreen() {
        if (transform.position.y > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y) {
            return false;
        }

        return true;
    }
}
