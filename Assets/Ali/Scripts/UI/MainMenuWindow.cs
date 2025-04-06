using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button optionsGameButton;
    [SerializeField] private Button exitGameButton;

    public override void Initialize()
    {
        startGameButton.onClick.AddListener(StartGame);
        optionsGameButton.onClick.AddListener(OpenOptions);
        exitGameButton.onClick.AddListener(ExitGame);
    }

    protected override void OpenEnd() => SetButtonsInteractable(true);
    protected override void CloseStart() => SetButtonsInteractable(false);

    private void StartGame()
    {
        GameManager.Instance.StartGame();
        GameManager.Instance.WindowsService.ShowWindow<GameplayWindow>(true);
        Hide(false);
    }

    private void OpenOptions()
    {
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
        startGameButton.interactable = state;
        optionsGameButton.interactable = state;
        exitGameButton.interactable = state;
    }
}
