public class FinalMechanic : IMechanic
{
    private Character character;
    public void Initialize(Character character) => this.character = character;
    public void Enable()
    {
        // Завершить игру, реализовать выбор игрока.
    }
    public void Disable()
    {
        // Логика отключения финальной механики (если требуется).
    }
}
