using UnityEngine;
using System.Collections;

public class AlarmLightFlicker : MonoBehaviour
{
    [SerializeField] private Light alarmLight;  // assign the Point Light
    [SerializeField] private float onIntensity = 6f;
    [SerializeField] private float offIntensity = 0f;
    [SerializeField] private float flickerInterval = 0.12f;
    [SerializeField] private int flickerCount = 14;
    [SerializeField] private bool onlyOnce = true;

    private bool triggered;
    private Coroutine routine;

    void Reset()
    {
        alarmLight = GetComponent<Light>();
        if (alarmLight) alarmLight.intensity = offIntensity;
    }

    public void Trigger()
    {
        if (onlyOnce && triggered) return;
        triggered = true;
        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        if (!alarmLight) yield break;
        for (int i = 0; i < flickerCount; i++)
        {
            alarmLight.intensity = (i % 2 == 0) ? onIntensity : offIntensity;
            yield return new WaitForSeconds(flickerInterval);
        }
        alarmLight.intensity = offIntensity;
        routine = null;
    }
}
