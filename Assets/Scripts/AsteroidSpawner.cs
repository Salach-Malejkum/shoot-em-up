using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private GameObject [] asteroids;
    [SerializeField] private float spawnTimer;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (timer > spawnTimer - Time.timeSinceLevelLoad / 100)
        {
            timer = 0;
            Vector2 screenLimits = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            Vector2 randPosition = new Vector2(Random.Range(-screenLimits.x, screenLimits.x), screenLimits.y + 1);
            Instantiate(asteroids[Random.Range(0, asteroids.Length)], randPosition, Quaternion.identity);
        }
        timer += Time.deltaTime;
    }
}
