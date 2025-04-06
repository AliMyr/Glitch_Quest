using UnityEngine;

public class CameraService : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float lerpSpeed;

    private void LateUpdate()
    {
        var gm = GameManager.Instance;
        if (gm?.player != null && Camera.main != null)
        {
            Vector3 targetPos = gm.player.transform.position + offset;
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, targetPos, lerpSpeed * Time.deltaTime);
        }
    }
}
