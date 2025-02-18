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
        this.stopLights.SetActive(Input.GetKey(KeyCode.DownArrow));
        if (Input.GetKey(KeyCode.UpArrow))
            this.forwardSpeed += this.speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.DownArrow))
            this.forwardSpeed -= this.speed * Time.deltaTime;

        this.forwardSpeed = Mathf.Clamp(this.forwardSpeed, minSpeed, maxSpeed);

        // Adjust steering speed
        this.currentSteeringSpeed = this.steeringSpeed * Mathf.Pow(this.forwardSpeed / this.maxSpeed, 0.8f);

        Vector3 pos = this.road.transform.position;
        pos.z += this.forwardSpeed * Time.deltaTime;

        this.isRoadRestarted = pos.z > 137.5f;

        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.position = this.SteeringPosition(false);
        else if (Input.GetKey(KeyCode.RightArrow))
            this.transform.position = this.SteeringPosition(true);

        if (this.isRoadRestarted)
            pos.z = -25.0f;

        this.road.transform.position = pos;
    }

    private Vector3 SteeringPosition(bool steerRight)
    {
        Vector3 pos = this.transform.position;
        pos.x += this.currentSteeringSpeed * Time.deltaTime * (steerRight ? -1 : 1);
        return pos;
    }

    public bool IsRoadRestarted() => this.isRoadRestarted;
}
