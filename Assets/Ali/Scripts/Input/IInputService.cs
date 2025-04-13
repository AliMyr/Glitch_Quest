using UnityEngine;

public interface IInputService
{
    Vector2 Direction { get; }
    bool Jump { get; }
    bool Use { get; }
    bool Throw { get; }
    
    // Методы для установки значений
    void SetDirection(Vector2 direction);
    void SetJump(bool value);
    void SetUse(bool value);
    void SetThrow(bool value);
    void ResetActions();
}
