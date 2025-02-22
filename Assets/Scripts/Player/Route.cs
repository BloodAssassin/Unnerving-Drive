using UnityEngine;
using UnityEngine.UI;

public class Route : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] Slider routeSlider;
    public float destination;

    private float currDistance;
    private float currSpeed;

    void Update()
    {
        if (currDistance / destination < 1)
        {
            UpdateRoute();
        }
    }

    private void UpdateRoute()
    {
        if (SceneLoader.gamePaused)
        {
            return;
        }

        currSpeed = player.forwardSpeed;
        float distancePerFrame = currSpeed * Time.deltaTime;
        currDistance += distancePerFrame;

        routeSlider.value = currDistance / destination;

        // Reload scene
        if (routeSlider.value == 1)
        {
            SceneLoader.level++;
            sceneLoader.NextLevel();
        }
    }
}
