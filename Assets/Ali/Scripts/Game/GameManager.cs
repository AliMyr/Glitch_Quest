using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WindowsService windowsService;
    private bool gameActive;

    public static GameManager Instance { get; private set; }
    public WindowsService WindowsService => windowsService;
    public bool IsGameActive => gameActive;
    public Character Player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            windowsService.Initialize();
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        gameActive = false;
    }

    public void StartGame()
    {
        if (gameActive || Player == null) return;
        gameActive = true;
        Player.Initialize();
    }
}
