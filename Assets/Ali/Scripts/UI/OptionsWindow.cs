using UnityEngine;
using UnityEngine.UI;

public class OptionsWindow : Window
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle soundsToggle;
    [SerializeField] private Button closeButton;

    public override void Initialize()
    {
        musicToggle.onValueChanged.AddListener(ToggleMusic);
        soundsToggle.onValueChanged.AddListener(ToggleSounds);
        closeButton.onClick.AddListener(CloseOptions);
    }

    protected override void OpenStart()
    {
        base.OpenStart();
    }


    protected override void CloseStart()
    {
        base.CloseStart();
    }

    protected override void OpenEnd()
    {
        base.OpenEnd();
    }

    protected override void CloseEnd()
    {
        base.CloseEnd();
    }

    private void CloseOptions()
    {
        Hide(true);
        GameManager.Instance.WindowsService.ShowWindow<MainMenuWindow>(false);
    }
    private void ToggleSounds(bool isEnabled)
    {
        SimpleAudioService.Instance.SetVolume(AudioSystemType.Sounds, isEnabled);
        SimpleAudioService.Instance.SetVolume(AudioSystemType.UISounds, isEnabled);
    }

    private void ToggleMusic(bool isEnabled)
    {
        SimpleAudioService.Instance.SetVolume(AudioSystemType.Ambient, isEnabled);
    }

}
