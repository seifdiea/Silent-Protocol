using TMPro;
using UnityEngine;

public class HeliosInteract : MonoBehaviour
{
    public GameObject promptUI;   // “Press E to overload HELIOS”
    public EmergencyManager emergency; // reference to the emergency system
    [SerializeField] private TextMeshProUGUI prompt;


    bool playerNear = false;

    private void Start()
    {
        promptUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            promptUI.SetActive(true);
            ShowPrompt("Press E to overload HELIOS");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            promptUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Player destroys HELIOS
            Debug.Log("HELIOS overloaded.");
            emergency.StartEmergency();
            Debug.Log("GM emergencyTriggered = " + GameManager.instance.emergencyTriggered);
            promptUI.SetActive(false);
            gameObject.SetActive(false); // or play explosion later
            GameManager.instance.destroyedHelios = true;
            GameManager.instance.emergencyTriggered = true;
        }
    }

    private void ShowPrompt(string text)
    {
        if (!prompt) return;
        if (!prompt.gameObject.activeSelf) prompt.gameObject.SetActive(true);
        prompt.text = text;
    }
}
