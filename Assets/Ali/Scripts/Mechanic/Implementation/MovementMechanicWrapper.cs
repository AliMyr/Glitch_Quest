using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MovementMechanicWrapper : IMechanic, IUpdatableMechanic
{
    private Character character;
    private bool isEnabled;

    public void Initialize(Character character)
    {
        this.character = character;

        if (character == null)
        {
            Debug.LogError("Character is null in MovementMechanicWrapper");
        }
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
    }

    public void Update() { }
}
