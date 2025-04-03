using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;

    public IMovableComponent MovableComponent { get; private set; }
    protected MechanicManager MechanicManager { get; private set; }

    public CharacterData CharacterData => characterData;
    public Transform CharacterTransform => characterTransform;
    public CharacterController CharacterController => characterController;

    public virtual void Initialize()
    {
        MovableComponent = new MoveComponent();
        MovableComponent.Initialize(this);

        MechanicManager = new MechanicManager();
        MechanicManager.Initialize(this);

        LevelManager.Instance.Initialize(MechanicManager);
        MechanicManager.ActivateMechanicsForLevel(LevelManager.Instance.CurrentLevel);
    }

    public abstract void Update();
}
