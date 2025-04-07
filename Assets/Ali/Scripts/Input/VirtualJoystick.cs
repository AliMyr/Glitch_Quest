using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image innerImage;
    [SerializeField] private Image outerImage;
    private Vector2 input = Vector2.zero;
    private float maxRadius;

    public Vector2 Direction => input != Vector2.zero ? input.normalized : Vector2.zero;

    private void Awake()
    {
        maxRadius = outerImage.rectTransform.sizeDelta.x * 0.5f;
    }

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            outerImage.rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint);
        input = localPoint;
        if (input.magnitude > maxRadius)
            input = input.normalized * maxRadius;
        innerImage.rectTransform.anchoredPosition = input;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        innerImage.rectTransform.anchoredPosition = Vector2.zero;
    }
}
