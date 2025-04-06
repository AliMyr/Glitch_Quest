using UnityEngine;

public interface IRotationComponent : ICharacterComponent
{
    bool IsRotationEnabled { get; set; }
    void Rotate(Vector3 direction);
}