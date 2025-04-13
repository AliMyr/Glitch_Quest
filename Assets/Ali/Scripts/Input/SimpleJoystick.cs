using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image innerImage;
    public Image outerImage;
    private Vector2 input;
    private float maxRadius;
    private Vector2 lastDirection = Vector2.zero;
    private float lastMagnitude = 0f;

    public Vector2 Direction => input.normalized;
    public float InputMagnitude => Mathf.Min(1, input.magnitude / maxRadius);

    private void Awake() 
    {
        maxRadius = outerImage.rectTransform.sizeDelta.x / 2f;
        Debug.Log($"SimpleJoystick: Initialized with maxRadius={maxRadius}");
    }

    private void Start()
    {
        // Проверяем наличие GameManager и InputService при старте
        if (GameManager.Instance == null)
        {
            Debug.LogError("SimpleJoystick: GameManager.Instance is null!");
        }
        else if (GameManager.Instance.InputService == null)
        {
            Debug.LogError("SimpleJoystick: GameManager.InputService is null!");
        }
        else
        {
            Debug.Log($"SimpleJoystick: Found InputService of type {GameManager.Instance.InputService.GetType().Name}");
        }
    }

    private void Update()
    {
        // Вызываем SetInputDirection каждый кадр, когда есть ввод
        // Это решает проблему, когда InputService или другие компоненты сбрасывают направление
        if (input.magnitude > 0)
        {
            SetInputDirection();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            outerImage.rectTransform, eventData.position, eventData.pressEventCamera, out input);
        
        // Ограничиваем, если выходит за пределы круга
        if (input.magnitude > maxRadius)
            input = input.normalized * maxRadius;
            
        // Обновляем положение внутреннего изображения
        innerImage.rectTransform.anchoredPosition = input;
        
        // Передаем обновленное направление в InputService
        SetInputDirection();
    }

    public void OnPointerDown(PointerEventData eventData) 
    {
        OnDrag(eventData);
        Debug.Log($"SimpleJoystick: Pointer down at {input}, Direction={Direction}");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Сбрасываем положение
        input = Vector2.zero;
        innerImage.rectTransform.anchoredPosition = Vector2.zero;
        
        Debug.Log("SimpleJoystick: Pointer up, reset to zero");
        
        // Сбрасываем направление в InputService
        SetInputDirection();
    }
    
    private void SetInputDirection()
    {
        // Получаем нормализованное направление и силу нажатия
        Vector2 directionToSend = Direction;
        float magnitude = InputMagnitude;
        
        // Логируем только при изменениях, чтобы не засорять консоль
        if (Vector2.Distance(directionToSend, lastDirection) > 0.05f || 
            Mathf.Abs(magnitude - lastMagnitude) > 0.05f)
        {
            lastDirection = directionToSend;
            lastMagnitude = magnitude;
            Debug.Log($"SimpleJoystick: Direction={directionToSend}, Magnitude={magnitude:F2}");
        }
        
        // Сначала проверяем, что GameManager и InputService существуют
        if (GameManager.Instance != null && GameManager.Instance.InputService != null)
        {
            // Применяем силу нажатия к направлению для более точного контроля
            directionToSend *= magnitude;
            
            // Устанавливаем направление в InputService
            GameManager.Instance.InputService.SetDirection(directionToSend);
        }
        else
        {
            Debug.LogWarning("SimpleJoystick: Cannot access InputService");
        }
    }
}