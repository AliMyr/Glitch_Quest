using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isPressed = false;

    private void Start()
    {
        // Проверяем доступность GameManager и InputService
        if (GameManager.Instance == null)
        {
            Debug.LogError("JumpButton: GameManager.Instance is null!");
        }
        else if (GameManager.Instance.InputService == null)
        {
            Debug.LogError("JumpButton: GameManager.InputService is null!");
        }
        else
        {
            Debug.Log($"JumpButton: Ready to use with {GameManager.Instance.InputService.GetType().Name}");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        SetJumpInput(true);
        Debug.Log("JumpButton: Pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        SetJumpInput(false);
        Debug.Log("JumpButton: Released");
    }

    private void SetJumpInput(bool value)
    {
        if (GameManager.Instance != null && GameManager.Instance.InputService != null)
        {
            GameManager.Instance.InputService.SetJump(value);
        }
        else
        {
            Debug.LogWarning("JumpButton: Cannot access InputService");
        }
    }

    private void OnDisable()
    {
        // Сбрасываем состояние прыжка при отключении кнопки
        if (isPressed)
        {
            isPressed = false;
            SetJumpInput(false);
        }
    }
}