using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionsGameButton;
    [SerializeField] private Button exitGameButton;

    public override void Initialize()
    {
        if (startGameButton != null)
        {
            startGameButton.onClick.AddListener(StartGame);
        }
        else
        {
            Debug.LogWarning("MainMenuWindow: startGameButton is not assigned");
        }

        if (optionsGameButton != null)
        {
            optionsGameButton.onClick.AddListener(OpenOptions);
        }
        else
        {
            Debug.LogWarning("MainMenuWindow: optionsGameButton is not assigned");
        }

        if (exitGameButton != null)
        {
            exitGameButton.onClick.AddListener(ExitGame);
        }
        else
        {
            Debug.LogWarning("MainMenuWindow: exitGameButton is not assigned");
        }

        Debug.Log("MainMenuWindow: Initialized");
    }

    protected override void OpenEnd() => SetButtonsInteractable(true);
    protected override void CloseStart() => SetButtonsInteractable(false);

    private void StartGame()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("MainMenuWindow: GameManager.Instance is null");
            return;
        }

        GameManager.Instance.StartGame();

        if (GameManager.Instance.WindowsService != null)
        {
            GameManager.Instance.WindowsService.ShowWindow<GameplayWindow>(true);
            Hide(false);
        }
        else
        {
            Debug.LogError("MainMenuWindow: WindowsService is null");
        }
    }

    private void OpenOptions()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("MainMenuWindow: GameManager.Instance is null in OpenOptions");
            return;
        }

        if (GameManager.Instance.WindowsService == null)
        {
            Debug.LogError("MainMenuWindow: WindowsService is null in OpenOptions");
            return;
        }

        Hide(false);
        GameManager.Instance.WindowsService.ShowWindow<OptionsWindow>(true);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void SetButtonsInteractable(bool state)
    {
        if (startGameButton != null) startGameButton.interactable = state;
        if (optionsGameButton != null) optionsGameButton.interactable = state;
        if (exitGameButton != null) exitGameButton.interactable = state;
    }
}