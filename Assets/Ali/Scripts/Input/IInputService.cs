using UnityEngine;

public interface IInputService
{
    Vector2 Direction { get; }
    bool Jump { get; }
    bool Use {  get; }
    bool Throw {  get; }
}
