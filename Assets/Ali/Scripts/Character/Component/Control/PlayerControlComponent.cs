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
        Vector3 horizontalMovement = MovementComponent.CalculateMovement(moveDirection);
        Vector3 verticalMovement = JumpComponent.CalculateJumpMovement(inputService.Jump);
        Vector3 totalMovement = (horizontalMovement + verticalMovement) * Time.deltaTime;
        character.CharacterController.Move(totalMovement);
        RotationComponent.Rotate(moveDirection);

        if (inputService.Use)
            InventoryComponent.PickupItem();
        if (inputService.Throw)
            InventoryComponent.DropItem();
    }
}
