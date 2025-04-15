using UnityEngine;

public class RendererVisibilityController : IVisibility
{
    private readonly Renderer[] renderers;

    public RendererVisibilityController(Renderer[] renderers)
    {
        this.renderers = renderers;
    }

    public void SetVisible(bool visible)
    {
        foreach (var r in renderers)
            r.enabled = visible;
    }
}
