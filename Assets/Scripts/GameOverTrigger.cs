using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject missionUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &&
            GameManager.instance.emergencyTriggered)
        {
            gameOverUI.SetActive(true);
            missionUI.SetActive(false);
            Time.timeScale = 0f;
        }
    }
}
