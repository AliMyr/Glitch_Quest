using UnityEngine;

public class JumpMechanic : IMechanic, IUpdatableMechanic
{
    private Character character;
    
    public float JumpForce {get; set;}
    public float Gravity {get; set;}
    public float VerticalVelocity { get; set;}


    public void Initialize(Character character)
    {
        this.character = character;
        this.JumpForce = character.CharacterData.JumpForce;
        this.Gravity = character.CharacterData.Gravity;
        this.VerticalVelocity = character.CharacterData.VerticalVelocity;
        
        VerticalVelocity = 0f;
    }

    public void Enable()
    {
        // Механика прыжка включена
    }

    public void Disable()
    {
        // Механика прыжка отключена
    }

    public void Update()
    {
        if (character.CharacterController.isGrounded)
        {
            VerticalVelocity = -1f;
            if (Input.GetButtonDown("Jump"))
                VerticalVelocity = JumpForce;
        }
        else
        {
            VerticalVelocity += Gravity * Time.deltaTime;
        }
        Vector3 jumpMovement = new Vector3(0, VerticalVelocity, 0);
        character.CharacterController.Move(jumpMovement * Time.deltaTime);
    }
}
