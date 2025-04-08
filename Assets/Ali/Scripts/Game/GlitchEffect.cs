using UnityEngine;

public class GlitchEffect : MonoBehaviour
{
    [SerializeField] private float glitchInterval = 5f;
    [SerializeField] private float glitchDuration = 0.3f;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= glitchInterval)
        {
            Renderer rend = GetComponent<Renderer>();
            if (rend != null)
            {
                rend.enabled = false;
                Invoke("ResetRenderer", glitchDuration);
            }
            timer = 0f;
        }
    }

    private void ResetRenderer()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
            rend.enabled = true;
    }
}
