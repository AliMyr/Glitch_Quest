using UnityEngine;

public class PlayerCharacter : Character
{
    private PlayerControlComponent controlComponent;
    private float debugTimer = 0f;

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("PlayerCharacter: Awake called");
    }

    public override void Initialize()
    {
        Debug.Log("PlayerCharacter: Initialize start");
        base.Initialize();

        if (MechanicManager == null)
        {
            Debug.LogError("PlayerCharacter: MechanicManager is null after base.Initialize()");
            return;
        }

        try
        {
            controlComponent = new PlayerControlComponent();

            if (GameManager.Instance == null)
            {
                Debug.LogError("PlayerCharacter: GameManager.Instance is null");
                return;
            }

            if (GameManager.Instance.InputService == null)
            {
                Debug.LogWarning("PlayerCharacter: InputService is null, creating a default one");
                GameManager.Instance.InputService = new UIInputService();
            }

            controlComponent.Initialize(this);

            // Регистрируем механики
            MechanicManager.RegisterMechanic(1, new MovementMechanicWrapper());
            MechanicManager.RegisterMechanic(2, new RotationMechanicWrapper());
            MechanicManager.RegisterMechanic(3, new JumpMechanicWrapper());
            MechanicManager.RegisterMechanic(4, new InventoryMechanicWrapper());
            MechanicManager.RegisterMechanic(5, new AnimationMechanicWrapper());
            MechanicManager.RegisterMechanic(6, new PhysicsMechanicWrapper());
            MechanicManager.RegisterMechanic(7, new FinalMechanicWrapper());

            // Убедимся, что инвентарь активирован, независимо от уровня
            if (InventoryComponent != null)
            {
                InventoryComponent.ActivateInventory();
                Debug.Log("PlayerCharacter: InventoryComponent forcefully activated");
            }

            CompleteInitialization();

            Debug.Log("PlayerCharacter: Successfully initialized");
        }
        catch (System.Exception e)
        {
            Debug.LogError("PlayerCharacter: Exception during initialization: " + e.Message);
        }
    }

    private void Update()
    {
        // Выходим, если компоненты не инициализированы
        if (MovementComponent == null || controlComponent == null || MechanicManager == null)
        {
            return;
        }

        try
        {
            // Прямое тестирование движения при нажатии клавиш
            TestDirectMovement();

            // Обновляем контроль и механики
            controlComponent.OnUpdate();
            MechanicManager.UpdateMechanics();

            // Сбрасываем действия в конце кадра
            if (GameManager.Instance != null && GameManager.Instance.InputService != null)
            {
                GameManager.Instance.InputService.ResetActions();
            }
            
            // Периодическая отладка
            LogDebugInfo();
        }
        catch (System.Exception e)
        {
            Debug.LogError("PlayerCharacter: Exception during update: " + e.Message);
        }
    }
    
    private void TestDirectMovement()
    {
        // Прямое тестирование с клавиатуры
        float speed = 5f;
        bool moved = false;
        Vector3 testMovement = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            testMovement += Vector3.forward * Time.deltaTime * speed;
            moved = true;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            testMovement += Vector3.back * Time.deltaTime * speed;
            moved = true;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            testMovement += Vector3.left * Time.deltaTime * speed;
            moved = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            testMovement += Vector3.right * Time.deltaTime * speed;
            moved = true;
        }
        
        if (moved && CharacterController != null && CharacterController.enabled)
        {
            CharacterController.Move(testMovement);
            Debug.Log("Applied direct keyboard movement: " + testMovement);
        }
    }
    
    private void LogDebugInfo()
    {
        debugTimer -= Time.deltaTime;
        if (debugTimer <= 0)
        {
            debugTimer = 2f; // Обновляем каждые 2 секунды
            
            // Получаем ввод от InputService для отладки
            Vector2 inputDirection = Vector2.zero;
            if (GameManager.Instance != null && GameManager.Instance.InputService != null)
            {
                inputDirection = GameManager.Instance.InputService.Direction;
            }
            
            if (inputDirection.magnitude > 0.1f)
            {
                Debug.Log("PlayerCharacter: Position=" + CharacterTransform.position + 
                          ", Input=" + inputDirection + 
                          ", CharacterController.enabled=" + (CharacterController?.enabled).ToString());
            }
        }
    }

    public override void CharacterUpdate() { }
}