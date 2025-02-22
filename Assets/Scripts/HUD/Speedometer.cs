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

        RotateDial();
    }

    private void RotateDial()
    {
        currSpeed = player.forwardSpeed;

        // Formula
        currRotation = currRotation - 40;

        // Apply rotation
        dial.transform.rotation = Quaternion.Euler(0, 0, currRotation);

        lastRotation = currRotation;
    }
}
