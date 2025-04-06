public class CharacterAnimationComponent : IAnimationComponent
{
    private Character character;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public void SetAnimation(string animationName) =>
        character?.Animator?.Play(animationName);

    public void SetTrigger(string triggerName) =>
        character?.Animator?.SetTrigger(triggerName);

    public void SetBool(string boolName, bool status) =>
        character?.Animator?.SetBool(boolName, status);

    public void SetValue(string valueName, float value) =>
        character?.Animator?.SetFloat(valueName, value);
}