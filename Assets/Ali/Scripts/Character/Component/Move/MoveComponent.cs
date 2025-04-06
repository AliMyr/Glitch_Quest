using UnityEngine;

public class MoveComponent : IMovableComponent
{
    private Character selfCharacter;
    private float turnSmoothVelocity;
    public bool RotationEnabled { get; set; }
    public float Speed { get; set; }
    public Vector3 Position
    {
        get => selfCharacter?.CharacterTransform.position ?? Vector3.zero;
        set
        {
            if (selfCharacter != null)
                selfCharacter.CharacterTransform.position = value;
        }
    }

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        Speed = selfCharacter.CharacterData.DefaultSpeed;
        RotationEnabled = false;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero || selfCharacter?.CharacterController == null) 
        {
            selfCharacter.AnimationComponent.SetValue("Movement", 0);
            return;
        }
        Vector3 movement = direction.normalized * Speed * Time.deltaTime;
        selfCharacter.CharacterController.Move(movement);

        selfCharacter.AnimationComponent.SetValue("Movement", Speed);
    }

    public void Rotate(Vector3 direction)
    {
        if (!RotationEnabled || direction == Vector3.zero || selfCharacter?.CharacterTransform == null)
            return;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.SmoothDampAngle(selfCharacter.CharacterTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
        selfCharacter.CharacterTransform.rotation = Quaternion.Euler(0, smoothedAngle, 0);
    }
}