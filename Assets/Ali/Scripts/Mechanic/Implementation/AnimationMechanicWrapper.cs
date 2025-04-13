using UnityEngine;

public class AnimationMechanicWrapper : IMechanic, IUpdatableMechanic
{
    private Character character;
    private bool isEnabled;
    private float lastSpeed = 0f;

    public void Initialize(Character character)
    {
        this.character = character;
        if (character == null)
        {
            Debug.LogError("Character is null in AnimationMechanicWrapper");
            return;
        }

        if (character.Animator == null)
        {
            Debug.LogError("Animator is null on character");
            return;
        }

        Debug.Log("AnimationMechanicWrapper initialized");
    }

    public void Enable()
    {
        isEnabled = true;
        Debug.Log("AnimationMechanicWrapper enabled");
        
        // Убедимся, что аниматор включен
        if (character != null && character.Animator != null && !character.Animator.enabled)
        {
            character.Animator.enabled = true;
            Debug.Log("Animator was disabled, enabled now");
        }
    }

    public void Disable()
    {
        isEnabled = false;
        Debug.Log("AnimationMechanicWrapper disabled");
    }
    
    public void Update()
    {
        if (!isEnabled || character == null || character.Animator == null)
            return;
            
        // Проверяем, что аниматор включен
        if (!character.Animator.enabled)
        {
            character.Animator.enabled = true;
            Debug.Log("Animator was disabled, re-enabled");
        }
        
        // Получаем данные о движении
        Vector2 inputDirection = Vector2.zero;
        if (GameManager.Instance != null && GameManager.Instance.InputService != null)
        {
            inputDirection = GameManager.Instance.InputService.Direction;
        }
        
        // Вычисляем скорость для анимации
        float speed = inputDirection.magnitude * (character.MovementComponent?.Speed ?? 5f);
        
        // Обновляем анимацию только при изменении скорости
        if (Mathf.Abs(speed - lastSpeed) > 0.1f)
        {
            character.AnimationComponent.SetValue("Movement", speed);
            lastSpeed = speed;
            Debug.Log("Updated animation speed to: " + speed);
        }
        
        // Проверяем, находится ли персонаж на земле
        bool isGrounded = character.CharacterController != null && character.CharacterController.isGrounded;
        character.AnimationComponent.SetBool("IsGrounded", isGrounded);
        
        // Проверяем, не упал ли персонаж (для анимации падения)
        if (character.JumpComponent != null)
        {
            float verticalVelocity = character.JumpComponent.VerticalVelocity;
            character.AnimationComponent.SetValue("VerticalVelocity", verticalVelocity);
            
            if (verticalVelocity < -0.5f && !isGrounded)
            {
                character.AnimationComponent.SetBool("IsFalling", true);
            }
            else
            {
                character.AnimationComponent.SetBool("IsFalling", false);
            }
        }
    }
}