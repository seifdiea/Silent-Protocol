using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool hasTalkedToDrLin = false;
    public bool hasKeycard = false;
    public bool doorUnlocked = false;
    public bool proceedtoLevel3 = false;
    public bool destroyedHelios = false;
    public bool emergencyTriggered = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
