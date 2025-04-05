using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    public int CurrentLevel { get; private set; } = 1;

    private MechanicManager mechanicManager;

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
        this.mechanicManager = manager;
        ActivateMechanicsForCurrentLevel();
    }

    public void TransitionToLevel(int level)
    {
        CurrentLevel = level;
        ActivateMechanicsForCurrentLevel();
        Debug.Log("Переход на уровень: " + CurrentLevel);
    }

    private void ActivateMechanicsForCurrentLevel()
    {
        if (mechanicManager != null)
            mechanicManager.ActivateMechanicsForLevel(CurrentLevel);
    }
}
