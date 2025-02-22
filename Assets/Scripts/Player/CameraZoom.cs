using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] PlayerController player;

    private float maxSpeed, minSpeed, currSpeed;

    void Start()
    {
        maxSpeed = player.maxSpeed;
        minSpeed = player.minSpeed;
    }

    void Update()
    {
        currSpeed = player.forwardSpeed;
        float speedPercentage = Mathf.Clamp01((currSpeed - minSpeed) / (maxSpeed - minSpeed));

        float newZoom = GetCameraZoom(speedPercentage);

        if (mainCamera.orthographicSize != newZoom)
        {
            AdjustZoom(newZoom);
        }
    }

    private void AdjustZoom(float newZoom)
    {
        LeanTween.cancel(mainCamera.gameObject);
        LeanTween.value(mainCamera.gameObject, mainCamera.orthographicSize, newZoom, 0.5f).setOnUpdate((float val) =>
        {
            mainCamera.orthographicSize = val;
        });
    }

    private float GetCameraZoom(float speedPercentage)
    {
        return 15f + (speedPercentage * 4f);
    }
}
