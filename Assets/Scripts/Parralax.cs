using UnityEngine;

public class Parralax : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * parallaxSpeed * Time.deltaTime);
    }
}
