using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float asteroidSpeed;

    private void Update()
    {
        transform.Translate(asteroidSpeed * Time.deltaTime * Vector2.down);

        if (!CheckIfOnScreen()) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }

    private bool CheckIfOnScreen() {
        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y) {
            return false;
        }

        return true;
    }
}
