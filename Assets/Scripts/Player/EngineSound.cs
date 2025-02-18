using UnityEngine;

public class EngineSound : MonoBehaviour
{
    [SerializeField] AudioSource clicks; // 2.2 - 3.0
    [SerializeField] AudioSource pops; // 1.7 - 3.0
    [SerializeField] AudioSource airflow; // 1.7 - 3.0
    [SerializeField] PlayerController player;
    float maxSpeed, currentSpeed;

    private void FixedUpdate()
    {
        maxSpeed = player.maxSpeed;
        currentSpeed = player.forwardSpeed;

        float airflowValue = 2.3f * (currentSpeed / maxSpeed);
        float clicksValue = 10.0f * (currentSpeed / maxSpeed);

        clicks.pitch = clicksValue;
        pops.pitch = airflowValue;
        airflow.pitch = airflowValue;
    }
}
