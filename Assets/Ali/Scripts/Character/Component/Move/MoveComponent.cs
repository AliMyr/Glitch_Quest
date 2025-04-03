using UnityEngine;

public class MoveComponent : IMovableComponent
{
    private Character selfCharacter;
    private float turnSmoothVelocity;

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
        this.selfCharacter = selfCharacter ?? throw new System.ArgumentNullException(nameof(selfCharacter));
        this.Speed = selfCharacter.CharacterData.DefaultSpeed;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero || selfCharacter?.CharacterController == null)
            return;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 movement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        selfCharacter.CharacterController.Move(movement * Speed * Time.deltaTime);
    }

    public void Rotate(Vector3 direction)
    {
        if (direction == Vector3.zero || selfCharacter?.CharacterTransform == null)
            return;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(
            selfCharacter.CharacterTransform.eulerAngles.y,
            targetAngle,
            ref turnSmoothVelocity,
            0.1f
        );

        selfCharacter.CharacterTransform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
