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
        JumpForce = character.CharacterData.JumpForce;
        Gravity = character.CharacterData.Gravity;
        VerticalVelocity = 0f;
        isActive = false;
    }

    public void Enable() => isActive = true;
    public void Disable() => isActive = false;

    public Vector3 CalculateJumpMovement(bool jumpPressed)
    {
        if (!isActive) return Vector3.zero;
        if (character == null || character.CharacterController == null)
            return Vector3.zero;

        if (character.CharacterController.isGrounded)
        {
            VerticalVelocity = -1f;
            if (jumpPressed)
            {
                VerticalVelocity = JumpForce;
                character.AnimationComponent?.SetTrigger("JumpTrigger");
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
    }
}
