using UnityEngine;
using TMPro;


public class InteractRaycaster : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactMask = ~0; // everything
    [SerializeField] private TextMeshProUGUI prompt;       // Assign your TMP text in Inspector

    private Camera cam;
    private Keycard keycardInView;
    private DoorPanel panelInView;
    private PlayerInventory inventory;

    void Awake()
    {
        cam = GetComponent<Camera>();
        inventory = FindObjectOfType<PlayerInventory>();
        if (prompt != null) prompt.gameObject.SetActive(false);
    }

    void Update()
    {
        keycardInView = null;
        panelInView = null;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(cam.transform.position, cam.transform.forward * interactDistance, Color.green);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactMask, QueryTriggerInteraction.Ignore))
        {
            // 1) Keycard
            if (hit.collider.TryGetComponent(out Keycard keycard))
            {
                keycardInView = keycard;
                ShowPrompt("Press E to pick up keycard");

                if (Input.GetKeyDown(KeyCode.E))
                {
                    keycard.Pickup();
                    GameManager.instance.hasKeycard = true;
                }
                return;
            }

            // 2) Door Panel
            if (hit.collider.TryGetComponent(out DoorPanel panel))
            {
                panelInView = panel;
                string msg = panel.GetPrompt(inventory);
                ShowPrompt(msg);

                if (Input.GetKeyDown(KeyCode.E)) { 
                    panel.Use(inventory);
                    
                }
                return;
            }
        }

        HidePrompt();
    }

    private void ShowPrompt(string text)
    {
        if (!prompt) return;
        if (!prompt.gameObject.activeSelf) prompt.gameObject.SetActive(true);
        prompt.text = text;
    }

    private void HidePrompt()
    {
        if (prompt && prompt.gameObject.activeSelf) prompt.gameObject.SetActive(false);
    }
}
