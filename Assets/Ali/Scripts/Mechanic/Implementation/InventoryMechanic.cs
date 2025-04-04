public class InventoryMechanic : IMechanic
{
    private Character character;
    public void Initialize(Character character) => this.character = character;
    public void Enable()
    {
        // Включить работу инвентаря.
    }
    public void Disable()
    {
        // Выключить работу инвентаря.
    }
}
