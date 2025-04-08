using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private string[] dialogueLines;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float displayDuration = 3f;
    private float timer;
    private bool dialogueActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dialogueActive && dialogueLines.Length > 0)
        {
            int index = Random.Range(0, dialogueLines.Length);
            dialogueText.text = dialogueLines[index];
            dialogueText.gameObject.SetActive(true);
            timer = 0f;
            dialogueActive = true;
        }
    }

    private void Update()
    {
        if (dialogueActive)
        {
            timer += Time.deltaTime;
            if (timer >= displayDuration)
            {
                dialogueText.gameObject.SetActive(false);
                dialogueActive = false;
            }
        }
    }
}
