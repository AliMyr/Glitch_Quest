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

    public void Update()
    {
        if (!isActive) return;
        if (character == null || character.CharacterController == null)
            return;
        if (character.CharacterController.isGrounded)
        {
            VerticalVelocity = -1f;
            if (Input.GetButtonDown("Jump"))
            {
                VerticalVelocity = JumpForce;
                character.AnimationComponent?.SetTrigger("JumpTrigger");
            }
        }
        else
        {
            VerticalVelocity += Gravity * Time.deltaTime;
        }
        Vector3 jumpMovement = new Vector3(0, VerticalVelocity, 0);
        character.CharacterController.Move(jumpMovement * Time.deltaTime);
    }
}