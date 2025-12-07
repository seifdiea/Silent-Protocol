using UnityEngine;
using TMPro;

public class MissionUI : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;

    void Update()
    {
        if (!GameManager.instance.hasKeycard)
            objectiveText.text = "Objective: Find the keycard.";

        else if (!GameManager.instance.doorUnlocked)
            objectiveText.text = "Objective: Reach level 2 door.";

        else if (!GameManager.instance.hasTalkedToDrLin)
            objectiveText.text = "Objective: Speak to Dr. Lin.";

      
        else if (!GameManager.instance.proceedtoLevel3)
            objectiveText.text = "Objective: Reach level 3 door.";

        else if (!GameManager.instance.destroyedHelios)
            objectiveText.text = "Objective: Destroy Helios.";

        else
            objectiveText.text = "Objective: Escape the facility.";
    }
}
