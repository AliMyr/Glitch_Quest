using UnityEngine;
using UnityEngine.Audio;

public class SimpleAudioService : MonoBehaviour
{
    public static SimpleAudioService Instance { get; private set; }
    [SerializeField] private AudioMixer audioMixer;
    private const float DB_MIN = -80f;
    private const float DB_MAX = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SetVolume(AudioSystemType type, bool isEnabled)
    {
        SetVolume(type, isEnabled ? 1f : 0f);
    }

    public void SetVolume(AudioSystemType type, float volume)
    {
        string parameter = type.ToString();
        float dB = UnityEngine.Mathf.Lerp(DB_MIN, DB_MAX, volume);
        audioMixer.SetFloat(parameter, dB);
    }
}