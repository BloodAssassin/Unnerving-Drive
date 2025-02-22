using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] CanvasGroup sceneFade;
    public static bool gamePaused;

    void Start()
    {
        FadeScene();
    }

    private void FadeScene()
    {
        sceneFade.alpha = 1.0f;

        // Wait 0.2s
        LeanTween.value(gameObject, 0f, 1.0f, 0.2f).setOnComplete(() =>
        {
            LeanTween.value(gameObject, sceneFade.alpha, 0f, 0.4f).setOnUpdate((float val) =>
            {
                sceneFade.alpha = val;
            });

        });

    }

    private void LoadScene(string sceneName)
    {
        LeanTween.value(gameObject, sceneFade.alpha, 1.0f, 0.4f).setOnUpdate((float val) =>
        {
            sceneFade.alpha = val;

            if (val == 1.0f)
            {
                SceneManager.LoadScene(sceneName);
            }
        });
    }

    public void Play()
    {
        LoadScene("DesertRoad");
    }

    public void MainMenu()
    {
        LoadScene("MainMenu");
    }
}
