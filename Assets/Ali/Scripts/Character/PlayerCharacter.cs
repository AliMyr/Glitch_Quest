using UnityEngine;

public class PlayerCharacter : Character
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        MovableComponent.Move(moveDirection);
        MovableComponent.Rotate(moveDirection);
    }
}