using UnityEngine;

public class MoveComponent : IMovableComponent
{
    private Character character;
    private float turnSmoothVelocity;
    public bool RotationEnabled { get; set; }
    public float Speed { get; set; }

    public Vector3 Position
    {
        get => character?.CharacterTransform.position ?? Vector3.zero;
        set
        {
            if (character != null)
                character.CharacterTransform.position = value;
        }
    }

    public void Initialize(Character character)
    {
        this.character = character;
        Speed = character.CharacterData.DefaultSpeed;
        RotationEnabled = false;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero || character?.CharacterController == null)
        {
            character.AnimationComponent.SetValue("Movement", 0);
            return;
        }
        Vector3 movement = direction.normalized * Speed * Time.deltaTime;
        character.CharacterController.Move(movement);
        character.AnimationComponent.SetValue("Movement", Speed);
    }

    public void Rotate(Vector3 direction)
    {
        if (!RotationEnabled || direction == Vector3.zero || character?.CharacterTransform == null)
            return;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.SmoothDampAngle(character.CharacterTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
        character.CharacterTransform.rotation = Quaternion.Euler(0, smoothedAngle, 0);
    }
}
