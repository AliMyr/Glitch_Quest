public class AnimationMechanicWrapper : IMechanic
{
    private Character character;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public void Enable() { }
    public void Disable() { }
}
