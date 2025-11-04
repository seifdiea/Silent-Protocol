using System.Collections;

using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Parts")]
    [SerializeField] private Transform doorLeft;
    [SerializeField] private Transform doorRight;

    [Header("Open Settings")]
    [Tooltip("How far each half moves away from the center, in local units.")]
    [SerializeField] private float openDistance = 1.2f;

    [Tooltip("Units per second.")]
    [SerializeField] private float openSpeed = 2.0f;

    [Tooltip("Local slide axis on the DOOR ROOT (choose 1,0,0 or 0,0,1 typically).")]
    [SerializeField] private Vector3 slideAxis = Vector3.right;

    [Tooltip("Invert if a leaf moves the wrong way.")]
    [SerializeField] private int leftDirection = +1;    // +1 or -1
    [SerializeField] private int rightDirection = -1;   // +1 or -1

    [SerializeField] private bool startClosed = true;
    [SerializeField] private bool autoClose = false;
    [SerializeField] private float autoCloseDelay = 3f;

    private Vector3 leftClosedLocalPos, rightClosedLocalPos;
    private Vector3 leftOpenLocalPos, rightOpenLocalPos;
    private bool isOpen;
    private Coroutine moveRoutine;

    void Awake()
    {
        if (!doorLeft || !doorRight)
        {
            Debug.LogError("DoorController: Assign doorLeft and doorRight.");
            enabled = false; return;
        }

        // Normalize slide axis in DOOR ROOT local space
        var axis = slideAxis.sqrMagnitude < 0.0001f ? Vector3.right : slideAxis.normalized;

        leftClosedLocalPos = doorLeft.localPosition;
        rightClosedLocalPos = doorRight.localPosition;

        // Move each half along the SAME axis but opposite directions
        leftOpenLocalPos = leftClosedLocalPos + axis * (openDistance * Mathf.Sign(leftDirection));
        rightOpenLocalPos = rightClosedLocalPos + axis * (openDistance * Mathf.Sign(rightDirection));

        isOpen = !startClosed;
        if (isOpen) SetToOpenInstant(); else SetToClosedInstant();
    }

    public void ToggleDoor()
    {
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveDoor(!isOpen));
        if (autoClose && !isOpen) StartCoroutine(AutoCloseAfterDelay());
    }

    public void Open()
    {
        if (isOpen) return;
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveDoor(true));
        if (autoClose) StartCoroutine(AutoCloseAfterDelay());
    }

    public void Close()
    {
        if (!isOpen) return;
        if (moveRoutine != null) StopCoroutine(moveRoutine);
        moveRoutine = StartCoroutine(MoveDoor(false));
    }

    private IEnumerator MoveDoor(bool openTarget)
    {
        isOpen = openTarget;

        Vector3 lStart = doorLeft.localPosition;
        Vector3 rStart = doorRight.localPosition;
        Vector3 lEnd = openTarget ? leftOpenLocalPos : leftClosedLocalPos;
        Vector3 rEnd = openTarget ? rightOpenLocalPos : rightClosedLocalPos;

        float dist = Mathf.Max(Vector3.Distance(lStart, lEnd), Vector3.Distance(rStart, rEnd));
        float duration = (openSpeed <= 0.0001f) ? 0.01f : dist / openSpeed;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            float s = Mathf.SmoothStep(0f, 1f, t);
            doorLeft.localPosition = Vector3.Lerp(lStart, lEnd, s);
            doorRight.localPosition = Vector3.Lerp(rStart, rEnd, s);
            yield return null;
        }

        doorLeft.localPosition = lEnd;
        doorRight.localPosition = rEnd;
        moveRoutine = null;
    }

    private IEnumerator AutoCloseAfterDelay()
    {
        yield return new WaitForSeconds(autoCloseDelay);
        Close();
    }

    private void SetToOpenInstant()
    {
        doorLeft.localPosition = leftOpenLocalPos;
        doorRight.localPosition = rightOpenLocalPos;
    }

    private void SetToClosedInstant()
    {
        doorLeft.localPosition = leftClosedLocalPos;
        doorRight.localPosition = rightClosedLocalPos;
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // Visualize slide axis at the door root
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(slideAxis.normalized) * 0.75f);
        Gizmos.DrawSphere(transform.position + transform.TransformDirection(slideAxis.normalized) * 0.75f, 0.03f);
    }
#endif
}
