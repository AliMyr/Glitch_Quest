using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animatorController;
    [SerializeField] private InventoryComponent inventoryComponent;

    public IMovementComponent MovementComponent { get; private set; }
    public IRotationComponent RotationComponent { get; private set; }
    public IJumpComponent JumpComponent { get; private set; }
    public IInventoryComponent InventoryComponent { get; private set; }
    public IAnimationComponent AnimationComponent { get; private set; }
    protected MechanicManager MechanicManager { get; private set; }

    public CharacterData CharacterData => characterData;
    public Transform CharacterTransform => characterTransform;
    public CharacterController CharacterController => characterController;
    public Animator Animator => animatorController;

    public virtual void Initialize()
    {
        MovementComponent = new MovementComponent();
        MovementComponent.Initialize(this);

        RotationComponent = new RotationComponent();
        RotationComponent.Initialize(this);

        JumpComponent = new JumpComponent();
        JumpComponent.Initialize(this);

        if (inventoryComponent == null)
            inventoryComponent = new InventoryComponent();
        InventoryComponent = inventoryComponent;
        InventoryComponent.Initialize(this);

        AnimationComponent = new CharacterAnimationComponent();
        AnimationComponent.Initialize(this);

        MechanicManager = new MechanicManager();
        MechanicManager.Initialize(this);

        LevelManager.Instance.Initialize(MechanicManager);
        MechanicManager.ActivateMechanicsForLevel(LevelManager.Instance.CurrentLevel);
    }


    public abstract void CharacterUpdate();
}
