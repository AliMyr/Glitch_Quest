using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInputService : IInputService
{
    private PlayerInput playerInput;

    public Vector2 Direction => playerInput.gameplay_map.Movement.ReadValue<Vector2>();

    public bool Jump => playerInput.gameplay_map.Jump.ReadValue<bool>();

    public bool Use => playerInput.gameplay_map.Use.ReadValue<bool>();

    public bool Throw => playerInput.gameplay_map.Throw.ReadValue<bool>();

    public NewInputService() 
    {
        playerInput = new PlayerInput();
        playerInput.gameplay_map.Enable();
    }
}
