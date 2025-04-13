using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayWindow : Window
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private SimpleJoystick joystick;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button useButton;
    [SerializeField] private Button throwButton;

    private UIInputService uiInputService;

    public override void Initialize()
    {
        uiInputService = new UIInputService();
        GameManager.Instance.InputService = uiInputService;

        jumpButton.onClick.AddListener(() => uiInputService.SetJump(true));
        useButton.onClick.AddListener(() => uiInputService.SetUse(true));
        throwButton.onClick.AddListener(() => uiInputService.SetThrow(true));
    }

    private void Update()
    {
        uiInputService.SetDirection(joystick.Direction);
        levelText.text = "Level: " + LevelManager.Instance.CurrentLevel;
    }
}
