using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeCameraInput : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private CameraService cameraService;
    private Vector2 lastPosition;
    private bool isDragging;

    public void OnPointerDown(PointerEventData eventData)
    {
        lastPosition = eventData.position;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragging)
            return;

        Vector2 currentPosition = eventData.position;
        Vector2 delta = currentPosition - lastPosition;
        lastPosition = currentPosition;
        if (cameraService != null)
            cameraService.AddRotationDelta(delta);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
    }
}
