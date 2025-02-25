using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float health, maxHealth;
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

    bool upArrowWasPressed = false;
    bool downArrowWasPressed = false;

    void Start() => this.forwardSpeed = this.speed;

    void Update()
    {
        bool upArrowPressed = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        bool downArrowPressed = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        this.stopLights.SetActive(downArrowPressed);
        if (upArrowPressed && SceneLoader.gamePaused == false)
        {
            this.forwardSpeed += this.speed * Time.deltaTime;
            this.forwardSpeed = Mathf.Clamp(this.forwardSpeed, this.speed, this.maxSpeed);
            this.upArrowWasPressed = true;
        }
        else if (downArrowPressed && SceneLoader.gamePaused == false)
        {
            this.forwardSpeed -= this.speed * Time.deltaTime;
            this.forwardSpeed = Mathf.Clamp(this.forwardSpeed, this.minSpeed, this.speed);
            this.downArrowWasPressed = true;
        }
        else
        {
            if (this.upArrowWasPressed)
            {
                this.forwardSpeed -= this.speed * Time.deltaTime;
                this.upArrowWasPressed = this.forwardSpeed > this.speed;
            }
            else if (this.downArrowWasPressed)
            {
                this.forwardSpeed += this.speed * Time.deltaTime;
                this.downArrowWasPressed = this.forwardSpeed < this.speed;
            }
        }

        // Adjust steering speed
        this.currentSteeringSpeed = this.steeringSpeed * Mathf.Pow(this.forwardSpeed / this.maxSpeed, 0.8f);

        Vector3 pos = this.road.transform.position;
        pos.z += this.forwardSpeed * Time.deltaTime;

        this.isRoadRestarted = pos.z > 167.5f;

        // Car Tilt
        steerLeft = steerRight = false;

        // Turning
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            this.transform.position = this.SteeringPosition(false);
            steerLeft = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            this.transform.position = this.SteeringPosition(true);
            steerRight = true;
        }

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

    private void OnTriggerEnter(Collider collision)
    {
        this.health--;
        if (this.health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
