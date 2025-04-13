using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WindowsService windowsService;
    [SerializeField] private Character playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private bool useNewInputSystem = false; // Добавлено для выбора типа InputService

    private bool gameActive;
    private bool initialized = false;

    public static GameManager Instance { get; private set; }
    public WindowsService WindowsService => windowsService;
    public bool IsGameActive => gameActive;
    public Character Player { get; private set; }
    public IInputService InputService { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeServices();
            initialized = true;
            Debug.Log("GameManager: Instance created and initialized");
        }
        else
        {
            Debug.Log("GameManager: Duplicate instance destroyed");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Создаем InputService, если он не задан
        if (InputService == null)
        {
            if (useNewInputSystem)
            {
                Debug.Log("GameManager: Creating NewInputService");
                InputService = new NewInputService();
            }
            else
            {
                Debug.Log("GameManager: Creating UIInputService");
                InputService = new UIInputService();
            }
        }
    }

    private void InitializeServices()
    {
        if (windowsService != null)
        {
            windowsService.Initialize();
            Debug.Log("GameManager: WindowsService initialized");
        }
        else
        {
            Debug.LogError("GameManager: WindowsService is null");
        }

        // Инициализация начального состояния
        gameActive = false;
    }

    public void StartGame()
    {
        if (!initialized)
        {
            Debug.LogError("GameManager: Cannot start game - not initialized");
            return;
        }

        // Если игра уже активна - ничего не делаем
        if (gameActive)
        {
            Debug.Log("GameManager: Game already active");
            return;
        }

        // Проверяем наличие InputService
        if (InputService == null)
        {
            Debug.LogError("GameManager: InputService is null, cannot start game");
            return;
        }

        // Проверяем наличие игрока
        if (Player == null)
        {
            Debug.LogWarning("GameManager: Player not set, attempting to create");
            CreatePlayer();

            if (Player == null)
            {
                Debug.LogError("GameManager: Failed to create player");
                return;
            }
        }

        gameActive = true;
        Player.Initialize();
        Debug.Log("GameManager: Game started");
    }

    private void CreatePlayer()
    {
        // Проверяем наличие префаба
        if (playerPrefab == null)
        {
            Debug.LogError("GameManager: playerPrefab not set");
            return;
        }

        // Определяем позицию спавна
        Vector3 spawnPosition = playerSpawnPoint != null
            ? playerSpawnPoint.position
            : Vector3.zero;

        // Создаем игрока
        Player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        Debug.Log($"GameManager: Player created at {spawnPosition}");
    }

    public void PauseGame()
    {
        if (!gameActive) return;

        gameActive = false;
        Debug.Log("GameManager: Game paused");
    }

    public void ResumeGame()
    {
        if (gameActive) return;

        gameActive = true;
        Debug.Log("GameManager: Game resumed");
    }
}