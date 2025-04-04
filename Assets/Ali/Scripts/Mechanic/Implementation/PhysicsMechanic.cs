public class PhysicsMechanic : IMechanic
{
    private Character character;
    public void Initialize(Character character) => this.character = character;
    public void Enable()
    {
        // Настроить физику.
    }
    public void Disable()
    {
        // Отключить модификации физики.
    }
}
