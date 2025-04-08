using UnityEngine;

public class CompanionComponent : MonoBehaviour
{
    [SerializeField] private AudioClip talkClip;
    private AudioSource audioSource;
    private bool activated;
    private bool hasSpoken;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();
    }

    public void Activate()
    {
        activated = true;
        if (!hasSpoken && talkClip != null)
        {
            audioSource.clip = talkClip;
            audioSource.Play();
            hasSpoken = true;
        }
    }

    private void Update()
    {
        if (activated && GameManager.Instance?.Player != null)
        {
            Transform player = GameManager.Instance.Player.transform;
            transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(1, 1, 0), Time.deltaTime);
        }
    }
}
