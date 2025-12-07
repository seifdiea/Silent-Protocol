using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class ScreenFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // stays between scenes
    }

    public void FadeOutToScene(string sceneName)
    {
        StartCoroutine(FadeOutCoroutine(sceneName));
    }

    IEnumerator FadeOutCoroutine(string sceneName)
    {
        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        // Load the next scene
        SceneManager.LoadScene(sceneName);

        // Wait one frame so scene loads fully before fade in
        yield return null;

        // Fade in
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }
    }
}
