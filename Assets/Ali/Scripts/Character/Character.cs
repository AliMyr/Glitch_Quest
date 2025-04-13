using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animatorController;
    [SerializeField] private InventoryComponent inventoryComponent;

    public IMovementComponent MovementComponent { get; protected set; }
    public IRotationComponent RotationComponent { get; protected set; }
    public IJumpComponent JumpComponent { get; protected set; }
    public IInventoryComponent InventoryComponent { get; protected set; }
    public IAnimationComponent AnimationComponent { get; protected set; }
    public MechanicManager MechanicManager { get; protected set; }

    public CharacterData CharacterData => characterData;
    public Transform CharacterTransform => characterTransform;
    public CharacterController CharacterController => characterController;
    public Animator Animator => animatorController;

    public virtual void Initialize()
    {
        
    }

    public virtual void InitializeComponents()
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
    }

    public virtual void CompleteInitsializion()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.Initialize(MechanicManager);

            MechanicManager.ActivateMechanicsForLevel(LevelManager.Instance.CurrentLevel);

            print("Mechanics activated for level: " + LevelManager.Instance.CurrentLevel);
        }
    }

    public abstract void CharacterUpdate();
}
