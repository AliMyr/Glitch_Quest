public class AnimationMechanic : IMechanic
{
    private Character character;
    public void Initialize(Character character) => this.character = character;
    public void Enable()
    {
        // Включить исправленные анимации.
    }
    public void Disable()
    {
        // Отключить анимации.
    }
}
