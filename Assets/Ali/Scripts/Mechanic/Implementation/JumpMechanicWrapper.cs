using UnityEngine;

public class JumpMechanicWrapper : IMechanic, IUpdatableMechanic
{
    private Character character;
    private bool isEnabled;
    private float debugTimer = 0f;

    public void Initialize(Character character)
    {
        this.character = character;
        if (character == null)
        {
            Debug.LogError("Character is null in JumpMechanicWrapper");
            return;
        }

        if (character.JumpComponent == null)
        {
            Debug.LogError("JumpComponent is null on character");
            return;
        }

        Debug.Log("JumpMechanicWrapper initialized");
    }

    public void Enable()
    {
        isEnabled = true;
        if (character != null && character.JumpComponent != null)
        {
            character.JumpComponent.Enable();
            Debug.Log("JumpMechanicWrapper and JumpComponent enabled");
        }
    }

    public void Disable()
    {
        isEnabled = false;
        if (character != null && character.JumpComponent != null)
        {
            character.JumpComponent.Disable();
            Debug.Log("JumpMechanicWrapper and JumpComponent disabled");
        }
    }

    public void Update()
    {
        if (!isEnabled || character == null || character.JumpComponent == null)
            return;

        // Обновляем компонент прыжка
        character.JumpComponent.Update();

        // Получаем состояние кнопки прыжка
        bool jumpPressed = false;
        if (GameManager.Instance != null && GameManager.Instance.InputService != null)
        {
            jumpPressed = GameManager.Instance.InputService.Jump;
        }

        // Также проверяем нажатие кнопки Space на клавиатуре для тестирования
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            Debug.Log("Jump key pressed (Space)");

            // Для тестирования можно также напрямую установить Jump в InputService
            if (GameManager.Instance != null && GameManager.Instance.InputService != null)
            {
                GameManager.Instance.InputService.SetJump(true);
            }
        }

        // Применяем прыжок
        if (jumpPressed)
        {
            // Проверяем, находится ли персонаж на земле
            bool isGrounded = character.CharacterController != null && character.CharacterController.isGrounded;

            if (isGrounded)
            {
                // Явно устанавливаем вертикальную скорость для прыжка
                float jumpForce = character.CharacterData != null ? character.CharacterData.JumpForce : 5f;
                character.JumpComponent.VerticalVelocity = jumpForce;

                // Запускаем анимацию прыжка
                if (character.AnimationComponent != null)
                {
                    character.AnimationComponent.SetTrigger("JumpTrigger");
                    Debug.Log("Jump animation triggered");
                }

                Debug.Log("Jump executed with force: " + jumpForce);
            }
        }

        // Применяем вертикальное движение от прыжка/гравитации
        if (character.CharacterController != null)
        {
            Vector3 jumpMovement = character.JumpComponent.CalculateJumpMovement(jumpPressed);
            character.CharacterController.Move(jumpMovement * Time.deltaTime);
        }

        // Отладочное логирование
        debugTimer -= Time.deltaTime;
        if (debugTimer <= 0)
        {
            debugTimer = 1f;

            bool isGrounded = character.CharacterController != null && character.CharacterController.isGrounded;
            float verticalVelocity = character.JumpComponent.VerticalVelocity;

            if (!isGrounded || Mathf.Abs(verticalVelocity) > 0.1f)
            {
                Debug.Log("Jump status: isGrounded=" + isGrounded +
                          ", verticalVelocity=" + verticalVelocity +
                          ", level=" + (LevelManager.Instance?.CurrentLevel ?? 0));
            }
        }
    }
}