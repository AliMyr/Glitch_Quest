using UnityEngine;

public interface IMovementComponent : ICharacterComponent
{
    float Speed { get; set; }
    Vector3 Position { get; set; }
    void Move(Vector3 direction);
    Vector3 CalculateMovement(Vector3 direction);
}
