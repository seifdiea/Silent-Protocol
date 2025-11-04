using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool HasKeycard { get; private set; }

    public void GiveKeycard()
    {
        HasKeycard = true;
        Debug.Log("Keycard acquired.");
    }
}
