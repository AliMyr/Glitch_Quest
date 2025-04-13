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

    protected virtual void Awake()
    {
        // Инициализируем необходимые компоненты, если они не установлены
        if (characterTransform == null)
        {
            characterTransform = transform;
            Debug.Log("Character: Set characterTransform to this.transform");
        }
        
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
            if (characterController == null)
            {
                Debug.LogError("Character: CharacterController component not found!");
            }
            else
            {
                Debug.Log("Character: Found CharacterController component");
            }
        }
        
        // Создаем CharacterData по умолчанию, если он не назначен
        if (characterData == null)
        {
            Debug.LogWarning("Character: CharacterData not assigned, using default values");
            
            // Создаем временный объект
            characterData = ScriptableObject.CreateInstance<CharacterData>();
            
            // Задаем значения по умолчанию через reflection
            // Это позволяет обойти private модификатор доступа
            System.Reflection.FieldInfo fieldSpeed = typeof(CharacterData).GetField("defaultSpeed", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
            if (fieldSpeed != null)
            {
                fieldSpeed.SetValue(characterData, 5.0f);
                Debug.Log("Character: Set defaultSpeed to 5.0f");
            }
            
            System.Reflection.FieldInfo fieldJump = typeof(CharacterData).GetField("jumpForce", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
            if (fieldJump != null)
            {
                fieldJump.SetValue(characterData, 5.0f);
                Debug.Log("Character: Set jumpForce to 5.0f");
            }
            
            System.Reflection.FieldInfo fieldGravity = typeof(CharacterData).GetField("gravity", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                
            if (fieldGravity != null)
            {
                fieldGravity.SetValue(characterData, -9.81f);
                Debug.Log("Character: Set gravity to -9.81f");
            }
        }
    }

    public virtual void Initialize()
    {
        Debug.Log("Character: Initialize start");
        
        // Проверка компонентов
        if (characterData == null)
        {
            Debug.LogError("Character: characterData is still null after Awake!");
            return;
        }
        
        if (characterTransform == null || characterController == null)
        {
            Debug.LogError("Character: Missing required components! TransformNull: " + (characterTransform == null) + ", ControllerNull: " + (characterController == null));
            return;
        }

        // Инициализация компонентов
        InitializeComponents();
        
        // Создание менеджера механик
        MechanicManager = new MechanicManager();
        MechanicManager.Initialize(this);
        
        Debug.Log("Character: Initialize complete");
    }
    
    protected virtual void InitializeComponents()
    {
        // Движение
        MovementComponent = new MovementComponent();
        MovementComponent.Initialize(this);
        Debug.Log("Character: MovementComponent initialized");

        // Вращение
        RotationComponent = new RotationComponent();
        RotationComponent.Initialize(this);
        Debug.Log("Character: RotationComponent initialized");

        // Прыжок
        JumpComponent = new JumpComponent();
        JumpComponent.Initialize(this);
        Debug.Log("Character: JumpComponent initialized");

        // Инвентарь
        if (inventoryComponent == null)
            inventoryComponent = new InventoryComponent();
        InventoryComponent = inventoryComponent;
        InventoryComponent.Initialize(this);
        Debug.Log("Character: InventoryComponent initialized");

        // Анимация
        AnimationComponent = new CharacterAnimationComponent();
        AnimationComponent.Initialize(this);
        Debug.Log("Character: AnimationComponent initialized");
    }

    // Метод для завершения инициализации после регистрации механик
    public virtual void CompleteInitialization()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.Initialize(MechanicManager);
            // Активируем механики для текущего уровня
            MechanicManager.ActivateMechanicsForLevel(LevelManager.Instance.CurrentLevel);
            Debug.Log($"Character: Mechanics activated for level {LevelManager.Instance.CurrentLevel}");
        }
        else
        {
            Debug.LogError("Character: LevelManager.Instance is null!");
        }
    }

    public abstract void CharacterUpdate();
}
