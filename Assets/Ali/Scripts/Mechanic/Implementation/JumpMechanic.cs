using UnityEngine;

public class JumpMechanic : IMechanic, IUpdatableMechanic
{
    private Character character;
    private float jumpForce = 5f;
    private float gravity = -9.81f;
    private float verticalVelocity;

    public void Initialize(Character character)
    {
        this.character = character;
        verticalVelocity = 0f;
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
            verticalVelocity = -1f;
            if (Input.GetButtonDown("Jump"))
                verticalVelocity = jumpForce;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        Vector3 jumpMovement = new Vector3(0, verticalVelocity, 0);
        character.CharacterController.Move(jumpMovement * Time.deltaTime);
    }
}
