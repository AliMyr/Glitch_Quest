public class JumpMechanic : IMechanic
{
    private Character character;
    public void Initialize(Character character) => this.character = character;
    public void Enable()
    {
        // Подписаться на ввод для прыжка, добавить реализацию прыжка.
    }
    public void Disable()
    {
        // Отписаться от прыжкового ввода.
    }
}
