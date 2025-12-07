using UnityEngine;

public class DoorLevel3 : MonoBehaviour
{
    public Animator doorAnimator;
    public string parameterName = "character_nearby";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        doorAnimator.SetBool(parameterName, true);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        doorAnimator.SetBool(parameterName, false);
        
    }
}
