using UnityEngine;

public class DesktopInputService : IInputService
{
    public Vector2 Direction => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    public bool Jump => Input.GetButtonDown("Jump");
    public bool Use => Input.GetKeyDown(KeyCode.E);
    public bool Throw => Input.GetKeyDown(KeyCode.Q);
    public Vector2 RotationDelta => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
}
