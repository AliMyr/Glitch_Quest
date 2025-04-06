using UnityEngine;

public class CameraService : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float lerpSpeed;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        var gm = GameManager.Instance;
        if (gm?.Player != null && mainCamera != null)
        {
            Vector3 targetPos = gm.Player.transform.position + offset;
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, lerpSpeed * Time.deltaTime);
        }
    }
}
