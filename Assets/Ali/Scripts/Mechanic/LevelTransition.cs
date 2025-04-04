using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            LevelManager.Instance.NextLevel();
            Debug.Log("Current Level: " + LevelManager.Instance.CurrentLevel);
        }
    }
}
