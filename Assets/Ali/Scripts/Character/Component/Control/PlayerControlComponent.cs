using UnityEngine;

public class PlayerControlComponent : IControlComponent
{
    private Character character;
    private IInputService inputService;
    private float lastInputMagnitude;

    public void Initialize(Character character)
    {
        this.character = character;
        Debug.Log("PlayerControlComponent: Initialize called");

        if (GameManager.Instance == null)
        {
            Debug.LogError("PlayerControlComponent: GameManager.Instance is null");
            return;
        }

        inputService = GameManager.Instance.InputService;
        if (inputService == null)
        {
            Debug.LogWarning("PlayerControlComponent: InputService is null, creating a default one");
            inputService = new UIInputService();
            GameManager.Instance.InputService = inputService;
        }
        
        Debug.Log($"PlayerControlComponent: Initialized with inputService={inputService.GetType().Name}");
    }

    public void OnUpdate()
    {
        // Проверки на null
        if (character == null || inputService == null)
        {
            Debug.LogWarning("PlayerControlComponent: character or inputService is null");
            return;
        }

        if (character.MovementComponent == null || character.JumpComponent == null ||
            character.RotationComponent == null || character.InventoryComponent == null)
        {
            Debug.LogWarning("PlayerControlComponent: One or more character components are null");
            return;
        }

        // Получаем ввод
        Vector2 inputDir = inputService.Direction;
        
        // Отлаживаем только при изменении величины ввода
        float currentMagnitude = inputDir.magnitude;
        if (Mathf.Abs(currentMagnitude - lastInputMagnitude) > 0.05f)
        {
            Debug.Log($"PlayerControlComponent: Input direction = {inputDir}, magnitude = {currentMagnitude}");
            lastInputMagnitude = currentMagnitude;
        }

        // Получаем камеру
        Camera cam = Camera.main;
        if (cam == null)
        {
            Debug.LogWarning("PlayerControlComponent: Main camera is null, using global directions");
            
            // Если нет камеры, просто используем глобальные направления
            Vector3 globalMoveDirection = new Vector3(inputDir.x, 0, inputDir.y);
            
            // Применяем движение
            ApplyMovement(globalMoveDirection);
            return;
        }

        // Вычисляем направление движения относительно камеры
        Vector3 camForward = cam.transform.forward; camForward.y = 0; camForward.Normalize();
        Vector3 camRight = cam.transform.right; camRight.y = 0; camRight.Normalize();
        
        // Создаем направление в мировых координатах
        Vector3 cameraMoveDirection = Vector3.zero;
        if (inputDir.magnitude > 0.1f) // Небольшой порог для исключения шума ввода
        {
            cameraMoveDirection = (camRight * inputDir.x + camForward * inputDir.y).normalized;
        }

        // Применяем движение
        ApplyMovement(cameraMoveDirection);
        
        // Обрабатываем действия
        HandleActions();
    }
    
    private void ApplyMovement(Vector3 moveDirection)
    {
        // Вычисляем вертикальное и горизонтальное движение
        Vector3 horizontalMovement = character.MovementComponent.CalculateMovement(moveDirection);
        Vector3 verticalMovement = character.JumpComponent.CalculateJumpMovement(inputService.Jump);
        Vector3 totalMovement = (horizontalMovement + verticalMovement) * Time.deltaTime;
        
        // Если есть значимое движение, применяем его и вращаем персонажа
        if (totalMovement.magnitude > 0.001f)
        {
            if (character.CharacterController != null)
            {
                character.CharacterController.Move(totalMovement);
                
                // Отладка только для значимого движения
                if (horizontalMovement.magnitude > 0.1f)
                {
                    Debug.Log($"PlayerControlComponent: Applied movement {totalMovement.magnitude:F2}");
                }
            }
            else
            {
                Debug.LogWarning("PlayerControlComponent: CharacterController is null");
            }
            
            // Если есть горизонтальное движение, применяем вращение
            if (moveDirection != Vector3.zero && horizontalMovement.magnitude > 0.001f)
            {
                character.RotationComponent.Rotate(moveDirection);
            }
        }
    }
    
    private void HandleActions()
    {
        // Взаимодействие с инвентарем
        if (inputService.Use)
        {
            character.InventoryComponent.PickupItem();
            Debug.Log("PlayerControlComponent: Character using item");
        }
        
        if (inputService.Throw)
        {
            character.InventoryComponent.DropItem();
            Debug.Log("PlayerControlComponent: Character throwing item");
        }
    }
}