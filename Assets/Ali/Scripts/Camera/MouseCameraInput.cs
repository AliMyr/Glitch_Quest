using UnityEngine;

public class MouseCameraInput : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 2.0f;
    private CameraService camService;
    private IInputService inputService;

    private void Awake()
    {
        camService = GetComponent<CameraService>();
    }

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            return;
        }
        inputService = GameManager.Instance.InputService;
    }

    private void Update()
    {
        if (!Application.isMobilePlatform && camService != null && inputService != null)
        {
            Vector2 scaledDelta = inputService.RotationDelta * mouseSensitivity;
            camService.AddRotationDelta(scaledDelta);
        }
    }
}
