using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameplayWindow : Window
{
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private VirtualJoystick movementJoystick;
    [SerializeField] private Button jumpButton;
    [SerializeField] private Button useButton;
    [SerializeField] private Button throwButton;

    private UIInputService uiInputService;
    private Character player;

    public override void Initialize()
    {
        base.Initialize();
        uiInputService = new UIInputService();
        GameManager.Instance.InputService = uiInputService;
        jumpButton.onClick.AddListener(() => uiInputService.SetJump(true));
        useButton.onClick.AddListener(() => uiInputService.SetUse(true));
        throwButton.onClick.AddListener(() => uiInputService.SetThrow(true));
    }

    protected override void OpenStart()
    {
        base.OpenStart();
    }


    protected override void CloseStart()
    {
        base.CloseStart();
    }

    protected override void OpenEnd()
    {
        base.OpenEnd();
    }

    protected override void CloseEnd()
    {
        base.CloseEnd();
    }

    private void Update()
    {
        uiInputService.SetDirection(movementJoystick.Direction);
        levelText.text = "Level: " + LevelManager.Instance.CurrentLevel;
    }
}
