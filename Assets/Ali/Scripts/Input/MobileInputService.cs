using UnityEngine;

public class MobileInputService : IInputService
{
    private Vector2 direction;
    private bool jump;
    private bool use;
    private bool throwAction;

    public Vector2 Direction => direction;
    public bool Jump => jump;
    public bool Use => use;
    public bool Throw => throwAction;
    public Vector2 RotationDelta => Vector2.zero;

    public void SetDirection(Vector2 value)
    {
        direction = value;
    }
    public void SetJump(bool value)
    {
        jump = value;
    }
    public void SetUse(bool value)
    {
        use = value;
    }
    public void SetThrow(bool value)
    {
        throwAction = value;
    }
    public void ResetActions()
    {
        jump = false;
        use = false;
        throwAction = false;
    }
}
