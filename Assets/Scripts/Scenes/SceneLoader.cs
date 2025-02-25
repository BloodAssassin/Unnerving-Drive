using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] CanvasGroup sceneFade;
    public static bool gamePaused;
    public static int level = 1;

    void Start()
    {
        FadeScene();

        GetCurrentLevel();
    }

    private void FadeScene()
    {
        sceneFade.alpha = 1.0f;

        LeanTween.value(gameObject, 0f, 1.0f, 1f).setOnComplete(() =>
        {
            LeanTween.value(gameObject, sceneFade.alpha, 0f, 1f).setOnUpdate((float val) =>
            {
                sceneFade.alpha = val;
            });

        });

    }

    private void LoadScene(string sceneName)
    {
        LeanTween.value(gameObject, sceneFade.alpha, 1.0f, 1f).setOnUpdate((float val) =>
        {
            sceneFade.alpha = val;

            if (val == 1.0f)
            {
                SceneManager.LoadScene(sceneName);
            }
        });
    }

    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        switch (level)
        {
            case 1:
                LoadScene("DesertRoad");
                break;
            case 2:
                LoadScene("DesertRoad_2");
                break;
            case 3:
                LoadScene("RainyForest");
                break;
            case 4:
                LoadScene("RainyForest_2");
                break;
            case 5:
                LoadScene("SnowyArea");
                break;
            case 6:
                LoadScene("SnowyArea_2");
                break;
            case 7:
                LoadScene("MeteorSite");
                break;
            case 8:
                LoadScene("EndScene");
                break;
            default:
                LoadScene("MainMenu");
                break;
        }
    }

    private void GetCurrentLevel()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "DesertRoad":
                level = 1;
                break;
            case "DesertRoad_2":
                level = 2;
                break;
            case "RainyForest":
                level = 3;
                break;
            case "RainyForest_2":
                level = 4;
                break;
            case "SnowyArea":
                level = 5;
                break;
            case "SnowyArea_2":
                level = 6;
                break;
            case "MeteorSite":
                level = 7;
                break;
            case "EndScene":
                level = 8;
                break;
            case "MainMenu":
                level = 1;
                break;
        }
    }

    public void Play()
    {
        LoadScene("DesertRoad");
    }

    public void MainMenu()
    {
        LoadScene("MainMenu");
    }

    public void Quit() => Application.Quit();
}
