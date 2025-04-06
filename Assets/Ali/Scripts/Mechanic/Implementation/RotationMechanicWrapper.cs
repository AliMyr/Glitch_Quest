public class RotationMechanicWrapper : IMechanic
{
    private Character character;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public void Enable()
    {
        if (character.RotationComponent != null)
            character.RotationComponent.IsRotationEnabled = true;
    }

    public void Disable()
    {
        if (character.RotationComponent != null)
            character.RotationComponent.IsRotationEnabled = false;
    }
}
