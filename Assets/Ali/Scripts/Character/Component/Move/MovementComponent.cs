using UnityEngine;

public class MovementComponent : IMovementComponent
{
    private Character character;
    public float Speed { get; set; }
    public Vector3 Position
    {
        get => character?.CharacterTransform.position ?? Vector3.zero;
        set { if (character != null) character.CharacterTransform.position = value; }
    }

    public void Initialize(Character character)
    {
        this.character = character;
        Speed = character.CharacterData.DefaultSpeed;
    }

    public Vector3 CalculateMovement(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            character.AnimationComponent.SetValue("Movement", 0);
            return Vector3.zero;
        }
        Vector3 movement = direction.normalized * Speed;
        character.AnimationComponent.SetValue("Movement", Speed);
        return movement;
    }

    public void Move(Vector3 direction)
    {
        if (character == null || character.CharacterController == null)
            return;
        Vector3 movement = CalculateMovement(direction);
        character.CharacterController.Move(movement * Time.deltaTime);
    }
}
