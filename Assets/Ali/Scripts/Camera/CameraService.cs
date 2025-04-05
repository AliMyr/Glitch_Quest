using UnityEngine;

public class CameraService : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float lerpSpeed;

    private void LateUpdate()
    {
        if (GameManager.Instance.player is { } player && Camera.main is { } mainCamera)
        {
            mainCamera.transform.position = Vector3.Lerp(
                mainCamera.transform.position,
                player.transform.position + offset,
                lerpSpeed * Time.deltaTime
            );
        }
    }
}