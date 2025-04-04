public class RotateMechanic : IMechanic
{
    private Character character;
    public void Initialize(Character character) => this.character = character;
    public void Enable()
    {
        if (character.MovableComponent is MoveComponent moveComponent)
            moveComponent.RotationEnabled = true;
    }
    public void Disable()
    {
        if (character.MovableComponent is MoveComponent moveComponent)
            moveComponent.RotationEnabled = false;
    }
}
