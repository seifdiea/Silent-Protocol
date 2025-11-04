using UnityEngine;

public class Keycard : MonoBehaviour
{
    [SerializeField] private AudioSource pickupSfx;   // optional
    [SerializeField] private Renderer[] renderersToHide;
    [SerializeField] private Collider[] collidersToDisable;

    private bool pickedUp;

    public void Pickup()
    {
        if (pickedUp) return;
        pickedUp = true;

        // Give to player
        var player = FindObjectOfType<PlayerInventory>();
        if (player != null)
        {
            player.GiveKeycard();
        }

        // Feedback
        if (pickupSfx != null) pickupSfx.Play();

        // Hide visuals & disable collisions
        if (renderersToHide != null)
            foreach (var r in renderersToHide) if (r != null) r.enabled = false;

        if (collidersToDisable != null)
            foreach (var c in collidersToDisable) if (c != null) c.enabled = false;

        // Optionally destroy after sound finishes
        float delay = (pickupSfx != null && pickupSfx.clip != null) ? pickupSfx.clip.length : 0f;
        Destroy(gameObject, delay > 0f ? delay : 0.05f);
    }

    // Auto-fill arrays if left empty (quality-of-life)
    void Reset()
    {
        renderersToHide = GetComponentsInChildren<Renderer>();
        collidersToDisable = GetComponentsInChildren<Collider>();
    }
}
