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
        mechanicManager = manager;
        ActivateMechanicsForCurrentLevel();
    }

    public void TransitionToLevel(int level)
    {
        CurrentLevel = level;
        ActivateMechanicsForCurrentLevel();
        Debug.Log("Transition to level: " + CurrentLevel);
    }

    private void ActivateMechanicsForCurrentLevel()
    {
        mechanicManager?.ActivateMechanicsForLevel(CurrentLevel);
    }
}
