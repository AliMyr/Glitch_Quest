using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class KeyboardMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Debug.Log("KeyboardMovement: Started - this is a direct keyboard controller for testing");
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            // Получаем ввод с клавиатуры
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            // Прыжок
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                Debug.Log("KeyboardMovement: Jump!");
            }
        }

        // Применяем гравитацию
        moveDirection.y -= gravity * Time.deltaTime;

        // Применяем движение
        controller.Move(moveDirection * Time.deltaTime);

        // Логируем движение, если оно не нулевое
        if (moveDirection.magnitude > 0.1f)
        {
            Debug.Log($"KeyboardMovement: Moving {moveDirection * Time.deltaTime}");
        }
    }

    // Для отладки коллизий
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log($"KeyboardMovement: Collided with {hit.gameObject.name}");
    }
}
