using UnityEngine;

public class UIInputService : IInputService
{
    public Vector2 Direction { get; private set; }
    public bool Jump { get; private set; }
    public bool Use { get; private set; }
    public bool Throw { get; private set; }

    public void SetDirection(Vector2 direction) 
    { 
        Direction = direction; 
        Debug.Log($"UIInputService: Direction set to {direction}"); 
    }
    
    public void SetJump(bool value) 
    {
        Jump = value;
        Debug.Log($"UIInputService: Jump set to {value}");
    }
    
    public void SetUse(bool value) 
    {
        Use = value;
        Debug.Log($"UIInputService: Use set to {value}");
    }
    
    public void SetThrow(bool value) 
    {
        Throw = value;
        Debug.Log($"UIInputService: Throw set to {value}");
    }
    
    public void ResetActions()
    {
        Jump = false;
        Use = false;
        Throw = false;
    }
}
