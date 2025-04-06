public class FinalMechanicWrapper : IMechanic, IUpdatableMechanic
{
    private Character character;
    public void Initialize(Character character) => this.character = character;
    public void Enable() { }
    public void Disable() { }
    public void Update() 
    {
        character.InventoryComponent?.Update(); 
    }
}
