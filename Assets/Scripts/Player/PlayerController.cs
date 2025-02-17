using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float steeringSpeed;
    [SerializeField] float speed;
    [SerializeField] GameObject road;
    float forwardSpeed;
    bool isRoadRestarted = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            this.forwardSpeed += this.speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.DownArrow))
            this.forwardSpeed -= this.speed * Time.deltaTime;

        this.forwardSpeed = Mathf.Clamp(this.forwardSpeed, 50.0f, 100.0f);

        Vector3 pos = road.transform.position;
        pos.z += this.forwardSpeed * Time.deltaTime;

        this.isRoadRestarted = pos.z > 137.5f;

        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.position = this.SteeringPosition(false);
        else if (Input.GetKey(KeyCode.RightArrow))
            this.transform.position = this.SteeringPosition(true);

        if (this.isRoadRestarted)
            pos.z = 0.0f;

        road.transform.position = pos;
    }

    private Vector3 SteeringPosition(bool steerRight)
    {
        Vector3 pos = this.transform.position;
        pos.x += this.steeringSpeed * Time.deltaTime * (steerRight ? -1 : 1);
        return pos;
    }

    public bool IsRoadRestarted() => this.isRoadRestarted;
}
