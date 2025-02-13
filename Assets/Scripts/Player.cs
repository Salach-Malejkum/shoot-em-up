using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform spawnerTransform;

    private Vector2 moveInput;
    
    private void Update() {
        Move();
    }

    private void OnMove(InputValue input) {
        moveInput = input.Get<Vector2>();
    }

    private void OnShoot() {
        Instantiate(bullet, spawnerTransform.position, Quaternion.identity);
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
}
