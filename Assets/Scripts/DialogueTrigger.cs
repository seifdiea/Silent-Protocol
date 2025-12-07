using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject dialogueUI;
    [TextArea]
    public string dialogueText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueUI.SetActive(true);
            dialogueUI.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = dialogueText;

            GameManager.instance.hasTalkedToDrLin = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueUI.SetActive(false);
        }
    }
}
