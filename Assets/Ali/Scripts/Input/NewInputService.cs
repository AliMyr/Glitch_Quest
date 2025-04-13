using UnityEngine;

public class NewInputService : IInputService
{
    private PlayerInput playerInput;
    private Vector2 manualDirection = Vector2.zero;
    private bool manualJump = false;
    private bool manualUse = false;
    private bool manualThrow = false;

    // Свойства из нового Input System
    public Vector2 Direction => manualDirection != Vector2.zero ? manualDirection : playerInput.gameplay_map.Movement.ReadValue<Vector2>();
    public bool Jump => manualJump || playerInput.gameplay_map.Jump.ReadValue<bool>();
    public bool Use => manualUse || playerInput.gameplay_map.Use.ReadValue<bool>();
    public bool Throw => manualThrow || playerInput.gameplay_map.Throw.ReadValue<bool>();

    public NewInputService()
    {
        playerInput = new PlayerInput();
        playerInput.gameplay_map.Enable();
        Debug.Log("NewInputService: Initialized with new Input System");
    }

    // Имплементация методов интерфейса IInputService
    public void SetDirection(Vector2 direction)
    {
        manualDirection = direction;
        Debug.Log($"NewInputService: Direction set to {direction}");
    }

    public void SetJump(bool value)
    {
        manualJump = value;
        Debug.Log($"NewInputService: Jump set to {value}");
    }

    public void SetUse(bool value)
    {
        manualUse = value;
        Debug.Log($"NewInputService: Use set to {value}");
    }

    public void SetThrow(bool value)
    {
        manualThrow = value;
        Debug.Log($"NewInputService: Throw set to {value}");
    }

    public void ResetActions()
    {
        manualJump = false;
        manualUse = false;
        manualThrow = false;
        // Не сбрасываем manualDirection - это делается отдельно при отпускании джойстика
        Debug.Log("NewInputService: Actions reset");
    }
}