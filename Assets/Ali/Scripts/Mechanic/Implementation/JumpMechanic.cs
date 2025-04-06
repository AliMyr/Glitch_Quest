using UnityEngine;

public class JumpMechanic : ICharacterComponent, IMechanic, IUpdatableMechanic
{
    private Character character;
    public float JumpForce { get; set; }
    public float Gravity { get; set; }
    public float VerticalVelocity { get; set; }

    public void Initialize(Character selfCharacter)
    {
        character = selfCharacter;
        JumpForce = selfCharacter.CharacterData.JumpForce;
        Gravity = selfCharacter.CharacterData.Gravity;
        VerticalVelocity = 0f;
    }

    public void Enable() { }

    public void Disable() { }

    public void Update()
    {
        if (character == null || character.CharacterController == null) return;

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
