using UnityEngine;

public class DoorPanel : MonoBehaviour
{
    [SerializeField] private DoorController door;
    [SerializeField] private string deniedMessage = "Access denied – keycard required";
    [SerializeField] private AudioSource beepOk;     // optional
    [SerializeField] private AudioSource beepDenied; // optional
    [SerializeField] private AlarmLightFlicker alarm; // assign in Inspector


    public string GetPrompt(PlayerInventory inv)
    {
        return inv != null && inv.HasKeycard
            ? "Press E to open door"
            : "Keycard required";
    }

    public void Use(PlayerInventory inv)
    {
        if (inv != null && inv.HasKeycard)
        {
            if (beepOk) beepOk.Play();
            if (door) door.ToggleDoor();
            if (alarm) alarm.Trigger();  // <-- add this line
            GameManager.instance.doorUnlocked = true;

        }

        else
        {
            if (beepDenied) beepDenied.Play();
            Debug.Log(deniedMessage);
        }
    }
}
