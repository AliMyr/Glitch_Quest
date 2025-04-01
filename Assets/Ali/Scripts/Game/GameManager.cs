using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isGameActive;
    public static GameManager Instance { get; private set; }

    public Character player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Initialize()
    {
        isGameActive = false;
    }

    public void StartGame()
    {
        if (isGameActive || player == null)
            return;

        isGameActive = true;
        player.Initialize();
    }
}
