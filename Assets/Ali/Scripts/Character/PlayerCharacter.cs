using UnityEngine;

public class PlayerCharacter : Character
{
    private PlayerControlComponent controlComponent;

    public override void Initialize()
    {
        base.Initialize();
        controlComponent = new PlayerControlComponent();
        controlComponent.Initialize(this);
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
        controlComponent.OnUpdate();
        MechanicManager.UpdateMechanics();
        if (GameManager.Instance.InputService is UIInputService ui)
            ui.ResetActions();
    }

    public override void CharacterUpdate() { }
}
