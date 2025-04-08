using UnityEngine;

public class FinalMechanicWrapper : IMechanic, IUpdatableMechanic
{
    private Character character;
    private bool companionActivated;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public void Enable() { }

    public void Disable() { }

    public void Update()
    {
        character.InventoryComponent?.Update();
        if (!companionActivated)
        {
            CompanionComponent companion = Object.FindObjectOfType<CompanionComponent>();
            if (companion != null)
            {
                companion.Activate();
                companionActivated = true;
            }
        }
    }
}
