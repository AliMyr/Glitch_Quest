using UnityEngine;

public class PlayerCharacter : Character
{
    public override void Initialize()
    {
        base.Initialize();

        MechanicManager.RegisterMechanic(2, new RotateMechanic());
        MechanicManager.RegisterMechanic(3, new JumpMechanic());
        MechanicManager.RegisterMechanic(4, new InventoryMechanic());
        MechanicManager.RegisterMechanic(5, new AnimationMechanic());
        MechanicManager.RegisterMechanic(6, new PhysicsMechanic());
        MechanicManager.RegisterMechanic(7, new FinalMechanic());
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