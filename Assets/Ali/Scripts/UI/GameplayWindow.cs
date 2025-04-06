using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayWindow : Window
{
    [SerializeField] private TMP_Text level ;

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

    private void Update()
    {
        if (LevelManager.Instance != null)
            level.text = $"Level: {LevelManager.Instance.CurrentLevel}";
    }
}
