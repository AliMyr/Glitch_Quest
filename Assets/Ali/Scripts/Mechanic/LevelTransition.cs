using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField] private int targetLevel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            LevelManager.Instance.TransitionToLevel(targetLevel);
    }
}
