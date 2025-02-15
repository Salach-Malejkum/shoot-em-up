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
        transform.Translate(asteroidSpeed * (Time.deltaTime + Time.timeSinceLevelLoad / 10000) * Vector2.down);

        if (!CheckIfOnScreen())
        {
            Destroy(gameObject);
        }
    }

    private bool CheckIfOnScreen()
    {
        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y - 5)
        {
            return false;
        }

        return true;
    }

    public void TakeDamage(string hitterTag)
    {
        if (--health <= 0 || hitterTag == "Player")
        {
            Destroy(gameObject);
            GameManager.Instance.AddScore(score);
        }
    }
}
