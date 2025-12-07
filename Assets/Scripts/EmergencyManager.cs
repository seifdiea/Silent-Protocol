using UnityEngine;
using UnityEngine.SceneManagement;

public class EmergencyManager : MonoBehaviour
{
    public Light[] warningLights;
    public float flashSpeed = 5f;

    private bool emergencyActive = false;

    void Update()
    {
        if (!emergencyActive) return;

        // Flash all warning lights
        foreach (var l in warningLights)
        {
            float intensity = Mathf.Abs(Mathf.Sin(Time.time * flashSpeed)) * 5;
            l.intensity = intensity;
        }
    }

    public void StartEmergency()
    {
        emergencyActive = true;
        GameManager.instance.destroyedHelios = true;
        GameManager.instance.emergencyTriggered = true;
    }
}
