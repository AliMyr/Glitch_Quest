using UnityEngine;

public class UIInputService : IInputService
{
    public Vector2 Direction { get; private set; }
    public bool Jump { get; private set; }
    public bool Use { get; private set; }
    public bool Throw { get; private set; }

    public void SetDirection(Vector2 direction) => Direction = direction;
    public void SetJump(bool jump) => Jump = jump;
    public void SetUse(bool use) => Use = use;
    public void SetThrow(bool value) => Throw = value;
    public void ResetActions()
    {
        Jump = false;
        Use = false;
        Throw = false;
    }
}
