using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image innerImage;
    public Image outerImage;
    private Vector2 input;
    private float maxRadius;

    public Vector2 Direction => input.normalized;

    private void Awake() => maxRadius = outerImage.rectTransform.sizeDelta.x / 2f;

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            outerImage.rectTransform, eventData.position, eventData.pressEventCamera, out input);
        if (input.magnitude > maxRadius)
            input = input.normalized * maxRadius;
        innerImage.rectTransform.anchoredPosition = input;
    }

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        innerImage.rectTransform.anchoredPosition = Vector2.zero;
    }
}
