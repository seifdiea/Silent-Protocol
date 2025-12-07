using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void ReturnToMain()
    {
        Time.timeScale = 1f; // reset in case game was paused
        SceneManager.LoadScene("MainMenu");
    }
}
