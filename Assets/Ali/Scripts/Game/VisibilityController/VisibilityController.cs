using UnityEngine;

[RequireComponent(typeof(Collider))]
public class VisibilityController : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    private IVisibility visibilityController;

    private void Awake()
    {
        var renderers = GetComponentsInChildren<Renderer>();
        visibilityController = new RendererVisibilityController(renderers);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            visibilityController.SetVisible(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
            visibilityController.SetVisible(true);
    }
}
