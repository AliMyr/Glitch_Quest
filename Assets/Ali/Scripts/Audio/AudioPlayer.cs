using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioMixerGroup ambientGroup;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = musicClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        if (ambientGroup != null)
            audioSource.outputAudioMixerGroup = ambientGroup;
    }

    private void Start()
    {
        SimpleAudioService.Instance.SetVolume(AudioSystemType.Ambient, true);
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
