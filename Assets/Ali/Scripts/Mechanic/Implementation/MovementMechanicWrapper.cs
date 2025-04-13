using UnityEngine;

public class MovementMechanicWrapper : IMechanic, IUpdatableMechanic
{
    private Character character;
    private bool isEnabled;
    private Vector3 lastPosition;
    private float debugTimer = 0f;

    public void Initialize(Character character)
    {
        this.character = character;
        if (character == null)
        {
            Debug.LogError("Character is null in MovementMechanicWrapper");
            return;
        }

        lastPosition = character.CharacterTransform.position;
        Debug.Log("MovementMechanicWrapper initialized");
    }

    public void Enable()
    {
        isEnabled = true;
        Debug.Log("MovementMechanicWrapper enabled");
    }

    public void Disable()
    {
        isEnabled = false;
        Debug.Log("MovementMechanicWrapper disabled");
    }

    public void Update()
    {
        if (!isEnabled || character == null)
            return;

        // Проверяем состояние компонентов
        if (character.CharacterController != null && !character.CharacterController.enabled)
        {
            character.CharacterController.enabled = true;
            Debug.Log("CharacterController was disabled, enabled now");
        }

        // Получаем входные данные от InputService
        Vector2 inputDirection = Vector2.zero;
        if (GameManager.Instance != null && GameManager.Instance.InputService != null)
        {
            inputDirection = GameManager.Instance.InputService.Direction;
        }

        // Если нет ввода от InputService, попробуем получить ввод напрямую
        if (inputDirection.magnitude < 0.1f)
        {
            float h = 0f, v = 0f;
            
            // Клавиши WASD и стрелки
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) v += 1f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) v -= 1f;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) h -= 1f;
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) h += 1f;
            
            if (h != 0f || v != 0f)
            {
                inputDirection = new Vector2(h, v).normalized;
                Debug.Log("Using direct keyboard input: " + inputDirection);
            }
        }

        // Применяем движение при наличии ввода
        if (inputDirection.magnitude > 0.1f)
        {
            // Получаем камеру
            Camera cam = Camera.main;
            Vector3 moveDirection;
            
            if (cam != null)
            {
                // Направление относительно камеры
                Vector3 camForward = cam.transform.forward; camForward.y = 0; camForward.Normalize();
                Vector3 camRight = cam.transform.right; camRight.y = 0; camRight.Normalize();
                moveDirection = (camRight * inputDirection.x + camForward * inputDirection.y).normalized;
            }
            else
            {
                // Глобальные координаты
                moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);
            }

            // Вычисляем скорость
            float speed = character.CharacterData != null ? character.CharacterData.DefaultSpeed : 5f;
            
            // Применяем движение
            Vector3 movement = moveDirection * speed * Time.deltaTime;
            
            if (character.CharacterController != null && character.CharacterController.enabled)
            {
                character.CharacterController.Move(movement);
                Debug.Log("Applied movement via CharacterController: " + movement);
                
                // Вращение персонажа
                if (character.RotationComponent != null)
                {
                    character.RotationComponent.Rotate(moveDirection);
                }
            }
            else if (character.CharacterTransform != null)
            {
                // Запасной вариант
                character.CharacterTransform.position += movement;
                Debug.Log("Applied movement via Transform: " + movement);
                
                // Вращение
                character.CharacterTransform.rotation = Quaternion.LookRotation(moveDirection);
            }
        }

        // Логируем движение периодически
        debugTimer -= Time.deltaTime;
        if (debugTimer <= 0)
        {
            debugTimer = 0.5f;
            
            Vector3 currentPosition = character.CharacterTransform.position;
            Vector3 movement = currentPosition - lastPosition;
            lastPosition = currentPosition;
            
            if (movement.magnitude > 0.01f || inputDirection.magnitude > 0.1f)
            {
                Debug.Log("Position: " + currentPosition + ", Movement: " + movement.magnitude + ", Input: " + inputDirection);
            }
        }
    }
}