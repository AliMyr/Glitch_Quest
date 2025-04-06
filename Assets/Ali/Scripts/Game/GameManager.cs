using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WindowsService windowsService;
    private bool isGameActive;

    public static GameManager Instance { get; private set; }
    public WindowsService WindowsService => windowsService;
    public bool IsGameActive => isGameActive;

    public Character player;

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

    private void Initialize() => isGameActive = false;


    public void StartGame()
    {
        if (isGameActive || player == null)
            return;

        isGameActive = true;
        player.Initialize();
    }
}
