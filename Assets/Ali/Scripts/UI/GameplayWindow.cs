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

    private MobileInputService mobileInputService;

    public override void Initialize()
    {
        mobileInputService = new MobileInputService();
        GameManager.Instance.InputService = mobileInputService;

        jumpButton.onClick.AddListener(() => mobileInputService.SetJump(true));
        useButton.onClick.AddListener(() => mobileInputService.SetUse(true));
        throwButton.onClick.AddListener(() => mobileInputService.SetThrow(true));
    }

    private void Update()
    {
        mobileInputService.SetDirection(joystick.Direction);
        levelText.text = "Level: " + LevelManager.Instance.CurrentLevel;
    }
}
