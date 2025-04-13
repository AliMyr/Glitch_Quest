using UnityEngine;

public class PlayerControlComponent : IControlComponent
{
    private Character character;
    private IInputService inputService;

    public void Initialize(Character character)
    {
        this.character = character;
        inputService = GameManager.Instance.InputService;
    }

    public void OnUpdate()
    {
        Vector2 inputDir = inputService.Direction;
        Camera cam = Camera.main;
        Vector3 camForward = cam.transform.forward; camForward.y = 0; camForward.Normalize();
        Vector3 camRight = cam.transform.right; camRight.y = 0; camRight.Normalize();
        Vector3 moveDirection = (camRight * inputDir.x + camForward * inputDir.y).normalized;

        Vector3 horizontalMovement = character.MovementComponent.CalculateMovement(moveDirection);
        Vector3 verticalMovement = character.JumpComponent.CalculateJumpMovement(inputService.Jump);
        Vector3 totalMovement = (horizontalMovement + verticalMovement) * Time.deltaTime;
        character.CharacterController.Move(totalMovement);

        if (moveDirection != Vector3.zero)
            character.RotationComponent.Rotate(moveDirection);

        if (inputService.Use)
            character.InventoryComponent.PickupItem();
        if (inputService.Throw)
            character.InventoryComponent.DropItem();
    }
}