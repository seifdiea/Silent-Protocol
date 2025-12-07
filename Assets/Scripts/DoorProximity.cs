using UnityEngine;

public class DoorProximity : MonoBehaviour
{
    public Animator doorAnimator;
    public string parameterName = "character_nearby";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        
        if (GameManager.instance.hasTalkedToDrLin)
        {
            doorAnimator.SetBool(parameterName, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instance.hasTalkedToDrLin)
        {
            doorAnimator.SetBool(parameterName, false);
        }
    }
}
