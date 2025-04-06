using UnityEngine;

public interface IJumpComponent : ICharacterComponent, IUpdatableMechanic
{
    float JumpForce { get; set; }
    float Gravity { get; set; }
    float VerticalVelocity { get; set; }
    void Enable();
    void Disable();
    Vector3 CalculateJumpMovement();
}
