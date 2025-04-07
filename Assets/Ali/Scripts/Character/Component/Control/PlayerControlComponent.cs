using UnityEngine;

public class PlayerControlComponent : IControlComponent
{
    private Character character;
    private IInputService inputService;

    private IMovementComponent MovementComponent => character.MovementComponent;
    private IRotationComponent RotationComponent => character.RotationComponent;
    private IInventoryComponent InventoryComponent => character.InventoryComponent;
    private IJumpComponent JumpComponent => character.JumpComponent;

    public void Initialize(Character character)
    {
        this.character = character;
        inputService = GameManager.Instance.InputService;
    }

    public void OnUpdate()
    {
        float x = inputService.Direction.x;
        float z = inputService.Direction.y;
        Vector3 moveDirection = new Vector3(x, 0, z).normalized;
        MovementComponent.Move(moveDirection);
        RotationComponent.Rotate(moveDirection);
        JumpComponent.Update();
        InventoryComponent.Update();
    }
}
