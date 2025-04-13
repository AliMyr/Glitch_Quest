using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WindowsService windowsService;
    [SerializeField] private Character playerPrefab;
    [SerializeField] private Transform playerSpawnPoint;

    private bool gameActive;

    public static GameManager Instance { get; private set; }
    public WindowsService WindowsService => windowsService;
    public bool IsGameActive => gameActive;
    public Character Player { get; private set; }
    public IInputService InputService { get; set; }

    private void Awake()
    {
        if (windowsService != null)
        {
            windowsService.Initialize();
        }
        else
        {
            Debug.LogError("GameManager: WindowsService is null");
        }

        if (InputService == null)
        {
            InputService = new UIInputService();
            Debug.LogError("GameManager: InputService is null");
        }

        gameActive = false;
    }

    public void StartGame()
    {
        if (gameActive || Player == null) return;
        gameActive = true;

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
    }

    public void PauseGame()
    {
        if (!gameActive) return;

        gameActive = false;
    }

    public void ResumeGame()
    {
        if (gameActive) return;
        gameActive = true;
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
}