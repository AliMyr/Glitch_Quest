using UnityEngine;

public class JumpComponent : IJumpComponent
{
    private Character character;
    public float JumpForce { get; set; }
    public float Gravity { get; set; }
    public float VerticalVelocity { get; set; }
    private bool isActive;

    public void Initialize(Character character)
    {
        this.character = character;

        if (character == null || character.CharacterData == null)
        {
            Debug.LogError("JumpComponent: Character or CharacterData is null");
            // Значения по умолчанию
            JumpForce = 5f;
            Gravity = -9.81f;
        }
        else
        {
            JumpForce = character.CharacterData.JumpForce;
            Gravity = character.CharacterData.Gravity;
        }

        VerticalVelocity = 0f;
        isActive = false;
        Debug.Log("JumpComponent: Initialized");
    }

    public void Enable()
    {
        isActive = true;
        Debug.Log("JumpComponent: Enabled");
    }

    public void Disable()
    {
        isActive = false;
        Debug.Log("JumpComponent: Disabled");
    }

    public Vector3 CalculateJumpMovement(bool jumpPressed)
    {
        // Проверки на null и активность
        if (!isActive) return Vector3.zero;
        if (character == null)
        {
            Debug.LogWarning("JumpComponent: Character is null");
            return Vector3.zero;
        }
        if (character.CharacterController == null)
        {
            Debug.LogWarning("JumpComponent: CharacterController is null");
            return Vector3.zero;
        }

        // Логика прыжка
        if (character.CharacterController.isGrounded)
        {
            VerticalVelocity = -1f; // Небольшая сила вниз для сохранения isGrounded

            if (jumpPressed)
            {
                VerticalVelocity = JumpForce;

                // Безопасный вызов триггера анимации
                if (character.AnimationComponent != null)
                {
                    character.AnimationComponent.SetTrigger("JumpTrigger");
                }
                else
                {
                    Debug.LogWarning("JumpComponent: AnimationComponent is null");
                }
            }
        }
        else
        {
            VerticalVelocity += Gravity * Time.deltaTime;
        }

        return new Vector3(0, VerticalVelocity, 0);
    }

    public void Update()
    {
        if (isActive && character != null && character.CharacterController != null)
        {
        }
    }
}