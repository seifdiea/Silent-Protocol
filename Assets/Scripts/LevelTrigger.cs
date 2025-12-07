using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public string nextSceneName;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Initiate.Fade(nextSceneName, Color.black, 1.0f);
            if (nextSceneName == "outpost with snow")
            {
                GameManager.instance.proceedtoLevel3 = true;
            }
        }
    }
}
