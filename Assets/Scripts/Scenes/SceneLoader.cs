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
                LoadScene("DesertRoad_3");
                break;
            case 4:
                LoadScene("RainyForest");
                break;
            case 5:
                LoadScene("RainyForest_2");
                break;
            case 6:
                LoadScene("SnowyArea");
                break;
            case 7:
                LoadScene("SnowyArea_2");
                break;
            case 8:
                LoadScene("MeteorSite");
                break;
            case 9:
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
            case "DesertRoad_3":
                level = 3;
                break;
            case "RainyForest":
                level = 4;
                break;
            case "RainyForest_2":
                level = 5;
                break;
            case "SnowyArea":
                level = 6;
                break;
            case "SnowyArea_2":
                level = 7;
                break;
            case "MeteorSite":
                level = 8;
                break;
            case "EndScene":
                level = 1;
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
}
