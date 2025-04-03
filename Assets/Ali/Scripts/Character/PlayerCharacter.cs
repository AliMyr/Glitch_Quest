using UnityEngine;

public class PlayerCharacter : Character
{
    public override void Initialize()
    {
        base.Initialize();

        //mechanicManager.RegisterMechanic(2, new RotateMechanic());
        //mechanicManager.RegisterMechanic(3, new JumpMechanic());
    }

    public override void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (MovableComponent == null)
            return;

        MovableComponent.Move(moveDirection);
        MovableComponent.Rotate(moveDirection);
    }
}