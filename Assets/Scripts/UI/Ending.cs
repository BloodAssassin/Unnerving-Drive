using UnityEngine;

public class Ending : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] CanvasGroup dialogue;
    [SerializeField] GameObject noise;
    [SerializeField] GameObject canvas;

    bool inputMade;
    bool scenePlayed;
    bool sceneFinished;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            inputMade = true;

        if (inputMade && dialogue.alpha == 0)
        {
            noise.SetActive(true);
            canvas.SetActive(true);
        }

        if (canvas.GetComponent<CanvasGroup>().alpha > 0)
        {
            scenePlayed = true;
        }

        if (scenePlayed && canvas.GetComponent<CanvasGroup>().alpha == 0)
        {
            sceneLoader.MainMenu();
            inputMade = false;
            scenePlayed = false;
        }
    }
}
