using UnityEngine;

public class RotationComponent : IRotationComponent
{
    private Character character;
    private float turnSmoothVelocity;
    public bool IsRotationEnabled { get; set; }

    public void Initialize(Character character)
    {
        this.character = character;
        IsRotationEnabled = false;
    }

    public void Rotate(Vector3 direction)
    {
        if (!IsRotationEnabled || direction == Vector3.zero || character?.CharacterTransform == null)
            return;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float smoothedAngle = Mathf.SmoothDampAngle(character.CharacterTransform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
        character.CharacterTransform.rotation = Quaternion.Euler(0, smoothedAngle, 0);
    }
}
