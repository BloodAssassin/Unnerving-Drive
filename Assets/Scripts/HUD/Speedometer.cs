using UnityEngine;

public class Speedometer : MonoBehaviour
{
    // 90 degrees <-> 50 km/h
    // - 90 degrees <-> 150 km/h

    [SerializeField] PlayerController player;
    [SerializeField] GameObject dial;

    private float minSpeed;
    private float maxSpeed;
    private float currSpeed;

    private float currRotation;
    private float lastRotation;

    private void Start()
    {
        minSpeed = player.minSpeed * 0.8f;
        maxSpeed = player.maxSpeed * 1.3f;
    }

    void Update()
    {
        currRotation = (currSpeed - minSpeed) * (-180) / (maxSpeed - minSpeed) + 90;

        if (lastRotation != currRotation)
        {
            RotateDial();
        }
    }

    private void RotateDial()
    {
        currSpeed = player.forwardSpeed;
        lastRotation = currRotation;

        // Formula
        currRotation = currRotation - 40;

        // Apply rotation
        LeanTween.cancel(dial);
        LeanTween.rotateZ(dial, currRotation, 0.1f);

        lastRotation = currRotation;
    }
}
