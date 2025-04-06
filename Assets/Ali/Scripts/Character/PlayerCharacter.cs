using UnityEngine;

public class PlayerCharacter : Character
{
    

    public override void Initialize()
    {
        base.Initialize();

        MechanicManager.RegisterMechanic(2, new RotationMechanicWrapper());
        MechanicManager.RegisterMechanic(3, new JumpMechanicWrapper());
        MechanicManager.RegisterMechanic(4, new InventoryMechanicWrapper());
        MechanicManager.RegisterMechanic(5, new AnimationMechanicWrapper());
        MechanicManager.RegisterMechanic(6, new PhysicsMechanicWrapper());
        MechanicManager.RegisterMechanic(7, new FinalMechanicWrapper());
    }

    private void Update()
    {
        if (MovementComponent == null) return;
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        Vector3 horizontalMovement = MovementComponent.CalculateMovement(moveDirection);
        Vector3 verticalMovement = JumpComponent.CalculateJumpMovement();
        Vector3 totalMovement = (horizontalMovement + verticalMovement) * Time.deltaTime;

        CharacterController.Move(totalMovement);
        RotationComponent.Rotate(moveDirection);
        MechanicManager.UpdateMechanics();
    }


    public override void CharacterUpdate() { }
}
