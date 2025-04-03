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

    public void Initialize(MechanicManager mechanicManager)
    {
        this.mechanicManager = mechanicManager;
        mechanicManager.ActivateMechanicsForLevel(CurrentLevel);
    }

    public void NextLevel()
    {
        CurrentLevel++;
        mechanicManager.ActivateMechanicsForLevel(CurrentLevel);
    }
}
