public class JumpMechanicWrapper : IMechanic, IUpdatableMechanic
{
    private Character character;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public void Enable()
    {
        if (character.JumpComponent != null)
            character.JumpComponent.Enable();
    }

    public void Disable()
    {
        if (character.JumpComponent != null)
            character.JumpComponent.Disable();
    }

    public void Update()
    {
        character.JumpComponent?.Update();
    }
}
