using UnityEngine;

public abstract class Window : MonoBehaviour
{
    [SerializeField] private string windowName;
    [SerializeField] private Animator windowAnimator;
    [SerializeField] protected string openAnimationName;
    [SerializeField] protected string idleAnimationName;
    [SerializeField] protected string closeAnimationName;
    [SerializeField] protected string hiddenAnimationName;

    public bool IsOpened { get; protected set; }
    private Animator Animator => windowAnimator ??= GetComponent<Animator>();

    public virtual void Initialize() { }

    public virtual void Show(bool immediate)
    {
        OpenStart();
        Animator.Play(immediate ? idleAnimationName : openAnimationName);
        if (immediate) OpenEnd();
    }

    public virtual void Hide(bool immediate)
    {
        CloseStart();
        if (Animator != null && gameObject.activeInHierarchy)
            Animator.Play(immediate ? hiddenAnimationName : closeAnimationName);
        if (immediate) CloseEnd();
    }

    protected virtual void OpenStart()
    {
        gameObject.SetActive(true);
        IsOpened = true;
    }
    protected virtual void OpenEnd() { }
    protected virtual void CloseStart() => IsOpened = false;
    protected virtual void CloseEnd() => gameObject.SetActive(false);
}
