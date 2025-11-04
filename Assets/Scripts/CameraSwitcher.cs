using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Camera firstPersonCam;
    [SerializeField] private Camera thirdPersonCam;
    [SerializeField] private Camera securityCam;      // assign your scene camera

    [Header("Keys")]
    [SerializeField] private KeyCode toggleViewKey = KeyCode.C;
    [SerializeField] private KeyCode cinematicKey = KeyCode.V;

    [Header("Cinematic")]
    [SerializeField] private float cinematicDuration = 3f; // seconds

    private Camera activeCam;
    private bool inCinematic;

    void Start()
    {
        SetActiveCam(firstPersonCam, true);
        SetActiveCam(thirdPersonCam, false);
        SetActiveCam(securityCam, false);
        activeCam = firstPersonCam;
    }

    void Update()
    {
        if (!inCinematic && Input.GetKeyDown(toggleViewKey))
        {
            bool toThird = (activeCam == firstPersonCam);
            SwitchTo(toThird ? thirdPersonCam : firstPersonCam);
        }

        if (!inCinematic && Input.GetKeyDown(cinematicKey) && securityCam)
        {
            StartCoroutine(CinematicPeek());
        }
    }

    private IEnumerator CinematicPeek()
    {
        inCinematic = true;
        var prev = activeCam;

        SwitchTo(securityCam);
        yield return new WaitForSeconds(cinematicDuration);
        SwitchTo(prev);

        inCinematic = false;
    }

    private void SwitchTo(Camera cam)
    {
        if (cam == activeCam || cam == null) return;
        SetActiveCam(activeCam, false);
        SetActiveCam(cam, true);
        activeCam = cam;
    }

    private void SetActiveCam(Camera cam, bool enable)
    {
        if (!cam) return;
        cam.gameObject.SetActive(enable);
        var al = cam.GetComponent<AudioListener>();
        if (al) al.enabled = enable;
    }
}
