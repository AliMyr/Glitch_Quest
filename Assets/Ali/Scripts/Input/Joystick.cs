using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform innerCircle;
    [SerializeField] private RectTransform outerCircle;
    private Vector2 input = Vector2.zero;
    private float maxRadius;

    public Vector2 Direction => input.normalized;

    private void Awake()
    {
        maxRadius = outerCircle.sizeDelta.x * 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            outerCircle, eventData.position, eventData.pressEventCamera, out pos);
        input = pos;
        if (input.magnitude > maxRadius)
            input = input.normalized * maxRadius;
        innerCircle.anchoredPosition = input;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        innerCircle.anchoredPosition = Vector2.zero;
    }
}
