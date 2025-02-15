using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float asteroidSpeed;
    [SerializeField] private int health;
    private int score;

    private void Start()
    {
        score = health;
    }

    private void Update()
    {
        transform.Translate(asteroidSpeed * Time.deltaTime * Vector2.down);

        if (!CheckIfOnScreen()) {
            Destroy(gameObject);
        }
    }

    private bool CheckIfOnScreen()
    {
        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y) {
            return false;
        }

        return true;
    }

    public void TakeDamage(string hitterTag)
    {
        if (--health <= 0 || hitterTag == "Player") {
            Destroy(gameObject);
            GameManager.Instance.AddScore(score);
        }
    }
}
