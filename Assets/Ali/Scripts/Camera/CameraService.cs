using UnityEngine;

public class CameraService : MonoBehaviour
{
    public static CameraService Instance { get; private set; }

    [SerializeField] private Vector3 offset = new Vector3(0, 5, -10);
    [SerializeField] private float lerpSpeed = 5f;
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float minPitch = -10f;
    [SerializeField] private float maxPitch = 45f;

    private Camera mainCamera;
    private float currentYaw;
    private float currentPitch;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        mainCamera = Camera.main;
        currentYaw = mainCamera.transform.eulerAngles.y;
        currentPitch = mainCamera.transform.eulerAngles.x;
    }

    public void AddRotationDelta(Vector2 delta)
    {
        currentYaw += delta.x * rotationSpeed;
        currentPitch -= delta.y * rotationSpeed;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);
    }

    private void LateUpdate()
    {
        var gm = GameManager.Instance;
        if (gm?.Player != null && mainCamera != null)
        {
            Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
            Vector3 targetPos = gm.Player.transform.position + rotation * offset;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, lerpSpeed * Time.deltaTime);
            mainCamera.transform.LookAt(gm.Player.transform.position);
        }
    }
}
