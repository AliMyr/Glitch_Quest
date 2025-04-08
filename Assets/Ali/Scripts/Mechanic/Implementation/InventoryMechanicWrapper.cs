public class InventoryMechanicWrapper : IMechanic, IUpdatableMechanic
{
    private Character character;
    private bool isActive = false;

    public void Initialize(Character character)
    {
        isActive = false;
        this.character = character;
    }

    public void Enable()
    {
        isActive = true;
    }

    public void Disable()
    {
        isActive = false;
    }

    public void Update()
    {
        if (!isActive)
            return;
        character.InventoryComponent?.Update();
    }
}
