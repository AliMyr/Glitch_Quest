using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] private GameObject inventoryItemPrefab;

    public override void Initialize()
    {
        base.Initialize();
        MechanicManager.RegisterMechanic(2, new RotateMechanic());
        MechanicManager.RegisterMechanic(3, new JumpMechanic());
        MechanicManager.RegisterMechanic(4, new InventoryMechanic(inventoryItemPrefab));
        MechanicManager.RegisterMechanic(5, new AnimationMechanic());
        MechanicManager.RegisterMechanic(6, new PhysicsMechanic());
        MechanicManager.RegisterMechanic(7, new FinalMechanic());
    }

    public override void Update()
    {
        if (MovableComponent == null) return;

        Vector3 moveDirection = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        MovableComponent.Move(moveDirection);
        MovableComponent.Rotate(moveDirection);
        MechanicManager.UpdateMechanics();
    }
}
