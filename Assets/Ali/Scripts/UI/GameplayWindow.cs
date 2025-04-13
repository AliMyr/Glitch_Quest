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
    private bool isInitialized = false;

    public override void Initialize()
    {
        Debug.Log("GameplayWindow: Initialize start");
        
        // Создаем новый UIInputService
        uiInputService = new UIInputService();
        Debug.Log($"GameplayWindow: Created new UIInputService {uiInputService.GetHashCode()}");
        
        // Проверяем компоненты на null
        if (joystick == null)
        {
            Debug.LogError("GameplayWindow: joystick is not assigned!");
        }
        else
        {
            Debug.Log("GameplayWindow: joystick is assigned");
        }
        
        // Инициализируем кнопки
        InitializeButtons();
        
        isInitialized = true;
        Debug.Log("GameplayWindow: Initialize complete");
    }

    private void InitializeButtons()
    {
        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(() => {
                uiInputService.SetJump(true);
                Debug.Log("GameplayWindow: Jump button clicked");
            });
        }
        else
        {
            Debug.LogWarning("GameplayWindow: jumpButton is not assigned");
        }

        if (useButton != null)
        {
            useButton.onClick.AddListener(() => {
                uiInputService.SetUse(true);
                Debug.Log("GameplayWindow: Use button clicked");
            });
        }
        else
        {
            Debug.LogWarning("GameplayWindow: useButton is not assigned");
        }

        if (throwButton != null)
        {
            throwButton.onClick.AddListener(() => {
                uiInputService.SetThrow(true);
                Debug.Log("GameplayWindow: Throw button clicked");
            });
        }
        else
        {
            Debug.LogWarning("GameplayWindow: throwButton is not assigned");
        }
    }

    private void Update()
    {
        if (!isInitialized) return;
        
        if (joystick != null && uiInputService != null)
        {
            Vector2 direction = joystick.Direction;
            if (direction.magnitude > 0.01f)
            {
                Debug.Log($"GameplayWindow: Joystick direction {direction}");
            }
            uiInputService.SetDirection(direction);
        }
        
        if (levelText != null && LevelManager.Instance != null)
        {
            levelText.text = "Level: " + LevelManager.Instance.CurrentLevel;
        }
        
        // Проверяем, что InputService в GameManager совпадает с нашим
        if (GameManager.Instance != null && GameManager.Instance.InputService != uiInputService)
        {
            Debug.LogWarning($"GameplayWindow: GameManager.InputService ({GameManager.Instance.InputService?.GetHashCode()}) is not same as our uiInputService ({uiInputService?.GetHashCode()})");
            GameManager.Instance.InputService = uiInputService;
        }
    }

    public override void Show(bool immediate)
    {
        base.Show(immediate);
        
        // При показе окна назначаем его InputService в GameManager
        if (GameManager.Instance != null && uiInputService != null)
        {
            GameManager.Instance.InputService = uiInputService;
            Debug.Log($"GameplayWindow: Set InputService {uiInputService.GetHashCode()} in GameManager on Show");
        }
        else
        {
            Debug.LogError("GameplayWindow: Could not set InputService - GameManager.Instance or uiInputService is null");
        }
    }

    public UIInputService GetInputService()
    {
        return uiInputService;
    }
}
