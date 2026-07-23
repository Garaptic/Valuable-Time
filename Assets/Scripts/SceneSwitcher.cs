using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance { get; private set; }

    [Header("References")]
    [SerializeField] CanvasGroup fadeCanvasGroup; // put on the full-screen Image's Canvas/parent

    [Header("Settings")]
    [SerializeField] float fadeDuration = 1f;
    [SerializeField] bool fadeInOnStart = true;

    void Awake()
    {
        // Simple singleton so the fader survives scene loads and isn't duplicated.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (fadeInOnStart)
        {
            StartCoroutine(Fade(1f, 0f)); // reveal the scene when it first loads
        }
    }

    /// <summary>
    /// Call this from a Button's OnClick event, passing the scene name to load.
    /// </summary>
    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeAndLoadRoutine(sceneName));
    }

    /// <summary>
    /// Overload if you prefer using build index instead of scene name.
    /// </summary>
    public void FadeAndLoadScene(int sceneBuildIndex)
    {
        StartCoroutine(FadeAndLoadRoutine(sceneBuildIndex));
    }

    IEnumerator FadeAndLoadRoutine(string sceneName)
    {
        yield return Fade(0f, 1f); // fade to black
        yield return SceneManager.LoadSceneAsync(sceneName);
        yield return Fade(1f, 0f); // fade back in on the new scene
    }

    IEnumerator FadeAndLoadRoutine(int sceneBuildIndex)
    {
        yield return Fade(0f, 1f);
        yield return SceneManager.LoadSceneAsync(sceneBuildIndex);
        yield return Fade(1f, 0f);
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        fadeCanvasGroup.blocksRaycasts = true; // block clicks while fading

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }

        fadeCanvasGroup.alpha = endAlpha;
        fadeCanvasGroup.blocksRaycasts = endAlpha > 0.5f; // only block if still opaque-ish
    }
}

