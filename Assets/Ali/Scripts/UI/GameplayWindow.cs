using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayWindow : Window
{
    [SerializeField] private TMP_Text levelText;

    private Character player;

    public override void Initialize()
    {
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

    private void Update()
    {
        if (LevelManager.Instance != null)
            levelText.text = "Level: " + LevelManager.Instance.CurrentLevel;
    }
}
