using UnityEngine;

public class PlayerCharacter : Character
{
    [SerializeField] private GameObject inventoryItemPrefab;

    public override void Initialize()
    {
        base.Initialize();
        mechanicManager.RegisterMechanic(2, new RotateMechanic());
        mechanicManager.RegisterMechanic(3, new JumpMechanic());
        mechanicManager.RegisterMechanic(4, new InventoryMechanic(inventoryItemPrefab));
        mechanicManager.RegisterMechanic(5, new AnimationMechanic());
        mechanicManager.RegisterMechanic(6, new PhysicsMechanic());
        mechanicManager.RegisterMechanic(7, new FinalMechanic());
    }

    private void Update()
    {
        if (MovableComponent == null) return;
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        MovableComponent.Move(moveDirection);
        MovableComponent.Rotate(moveDirection);
        mechanicManager.UpdateMechanics();
    }

    public override void CharacterUpdate() { }
}
