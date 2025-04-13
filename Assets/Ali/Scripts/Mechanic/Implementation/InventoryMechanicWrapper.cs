public class InventoryMechanicWrapper : IMechanic
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
        character.InventoryComponent.ActivateInventory();
    }

    public void Disable()
    {
        isActive = false;
        character.InventoryComponent.DeactivateInventory();
    }
}
