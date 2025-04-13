using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int CurrentLevel { get; private set; } = 1;
    private MechanicManager mechanicManager;
    private bool isInitialized = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(MechanicManager manager)
    {
        if (manager == null)
        {
            Debug.LogError("MechanicManager is null in LevelManager");
            return;
        }

        mechanicManager = manager;
        isInitialized = true;
        Debug.Log($"LevelManager: Initialized with current level {CurrentLevel}");
        ActivateMechanicsForCurrentLevel();
    }

    public void TransitionToLevel(int level)
    {
        CurrentLevel = level;
        Debug.Log("Transition to level: " + CurrentLevel);
        ActivateMechanicsForCurrentLevel();
        
    }

    private void ActivateMechanicsForCurrentLevel()
    {
        mechanicManager?.ActivateMechanicsForLevel(CurrentLevel);
    }
}
