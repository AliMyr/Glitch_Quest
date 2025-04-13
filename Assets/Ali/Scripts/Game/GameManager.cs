using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WindowsService windowsService;
    private bool gameActive;

    public static GameManager Instance { get; private set; }
    public WindowsService WindowsService => windowsService;
    public bool IsGameActive => gameActive;
    public Character Player;
    public IInputService InputService { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            windowsService.Initialize();
            Initialize();
            InputService = InputServiceFactory.Create();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize() => gameActive = false;

    public void StartGame()
    {
        if (gameActive || Player == null) return;
        gameActive = true;
        Player.Initialize();
    }
}
