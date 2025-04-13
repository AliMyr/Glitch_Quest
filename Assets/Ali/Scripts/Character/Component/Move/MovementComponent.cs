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
        
        if (character == null || character.CharacterData == null)
        {
            Debug.LogError("MovementComponent: Character or CharacterData is null!");
            Speed = 5f; // Значение по умолчанию, если CharacterData не найден
        }
        else
        {
            Speed = character.CharacterData.DefaultSpeed;
            Debug.Log($"MovementComponent: Initialized with Speed={Speed}");
        }
    }

    public Vector3 CalculateMovement(Vector3 direction)
    {
        if (character == null)
        {
            Debug.LogWarning("MovementComponent: Character is null in CalculateMovement");
            return Vector3.zero;
        }
        
        if (direction == Vector3.zero)
        {
            if (character.AnimationComponent != null)
            {
                character.AnimationComponent.SetValue("Movement", 0);
            }
            return Vector3.zero;
        }
        
        Vector3 movement = direction.normalized * Speed;
        
        if (character.AnimationComponent != null)
        {
            character.AnimationComponent.SetValue("Movement", Speed);
        }
        
        Debug.Log($"MovementComponent: Calculated movement: {movement} from direction: {direction}, speed: {Speed}");
        return movement;
    }

    public void Move(Vector3 direction)
    {
        if (character == null)
        {
            Debug.LogWarning("MovementComponent: Character is null in Move");
            return;
        }
        
        if (character.CharacterController == null)
        {
            Debug.LogWarning("MovementComponent: CharacterController is null in Move");
            return;
        }
        
        Vector3 movement = CalculateMovement(direction);
        Vector3 deltaMovement = movement * Time.deltaTime;
        
        character.CharacterController.Move(deltaMovement);
        Debug.Log($"MovementComponent: Moved character by {deltaMovement}");
    }
}
