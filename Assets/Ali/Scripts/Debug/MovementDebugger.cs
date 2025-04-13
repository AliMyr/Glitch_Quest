using UnityEngine;

public class MovementDebugger : MonoBehaviour
{
    public CharacterController characterController;
    public float testSpeed = 5f;

    private void Start()
    {
        Debug.Log("MovementDebugger: Started");

        if (characterController == null)
        {
            // Пробуем найти CharacterController на том же объекте
            characterController = GetComponent<CharacterController>();
            
            // Если не нашли, ищем в child объектах
            if (characterController == null) 
            {
                characterController = GetComponentInChildren<CharacterController>();
            }
            
            // Если и в child не нашли, ищем объект с тегом Player
            if (characterController == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    characterController = player.GetComponent<CharacterController>();
                    Debug.Log($"MovementDebugger: Found player with CharacterController: {characterController != null}");
                }
            }
        }

        if (characterController == null)
        {
            Debug.LogError("MovementDebugger: CharacterController not found!");
        }
        else
        {
            Debug.Log($"MovementDebugger: CharacterController found - stepOffset: {characterController.stepOffset}, radius: {characterController.radius}, height: {characterController.height}");
        }

        // Найдем GameManager и проверим, настроен ли InputService
        GameObject gm = GameObject.Find("GameManager");
        if (gm != null)
        {
            GameManager gameManager = gm.GetComponent<GameManager>();
            if (gameManager != null)
            {
                Debug.Log($"MovementDebugger: GameManager found, InputService: {gameManager.InputService != null}, IsGameActive: {gameManager.IsGameActive}");
            }
            else
            {
                Debug.LogError("MovementDebugger: GameManager component not found!");
            }
        }
        else
        {
            Debug.LogError("MovementDebugger: GameManager not found!");
        }
    }

    private void Update()
    {
        // Прямое тестирование движения с клавиатуры
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            movement += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            movement += Vector3.back;
        if (Input.GetKey(KeyCode.A))
            movement += Vector3.left;
        if (Input.GetKey(KeyCode.D))
            movement += Vector3.right;

        if (movement != Vector3.zero && characterController != null)
        {
            movement = movement.normalized * testSpeed * Time.deltaTime;
            
            // Применяем локальное движение относительно камеры
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                Vector3 camForward = mainCamera.transform.forward;
                camForward.y = 0;
                camForward.Normalize();
                
                Vector3 camRight = mainCamera.transform.right;
                camRight.y = 0;
                camRight.Normalize();
                
                movement = camRight * movement.x + camForward * movement.z;
            }
            
            characterController.Move(movement);
            Debug.Log($"MovementDebugger: Moving character: {movement}");
        }
    }
}
