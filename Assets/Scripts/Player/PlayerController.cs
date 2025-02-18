using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;

    [SerializeField] float steeringSpeed;
    [SerializeField] float speed;

    [SerializeField] GameObject stopLights;
    [SerializeField] GameObject road;
    [SerializeField] GameObject props;

    [HideInInspector] public bool steerLeft, steerRight;
    [HideInInspector] public float forwardSpeed;

    float currentSteeringSpeed;
    bool isRoadRestarted = false;

    // Update is called once per frame
    void Update()
    {
        stopLights.SetActive(false);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.forwardSpeed += this.speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.forwardSpeed -= this.speed * Time.deltaTime;
            stopLights.SetActive(true);
        }

        this.forwardSpeed = Mathf.Clamp(this.forwardSpeed, minSpeed, maxSpeed);

        // Adjust steering speed
        currentSteeringSpeed = steeringSpeed * Mathf.Pow(forwardSpeed / maxSpeed, 0.8f);

        Vector3 pos = road.transform.position;
        pos.z += this.forwardSpeed * Time.deltaTime;

        this.isRoadRestarted = pos.z > 137.5f;

        steerLeft = steerRight = false;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            steerLeft = true;
            this.transform.position = this.SteeringPosition(steerRight);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            steerRight = true;
            this.transform.position = this.SteeringPosition(steerRight);
        }


        if (this.isRoadRestarted)
            pos.z = 0.0f;

        road.transform.position = pos;
        MoveProps();
    }

    private Vector3 SteeringPosition(bool steerRight)
    {
        Vector3 pos = this.transform.position;
        pos.x += this.currentSteeringSpeed * Time.deltaTime * (steerRight ? -1 : 1);
        return pos;
    }

    private void MoveProps()
    {
        foreach (Transform prop in props.transform)
        {
            Vector3 pos = prop.transform.position;
            pos.z += this.forwardSpeed * Time.deltaTime;

            prop.transform.position = pos;
        }
    }

    public bool IsRoadRestarted() => this.isRoadRestarted;
}
