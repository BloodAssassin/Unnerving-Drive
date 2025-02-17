using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float steeringSpeed;
    [SerializeField] float speed;
    [SerializeField] GameObject road;
    float forwardSpeed;

    // Update is called once per frame
    void Update()
    {
        this.forwardSpeed = Input.GetKey(KeyCode.UpArrow) ? speed * 3 : speed;
        this.forwardSpeed = Input.GetKey(KeyCode.DownArrow) ? speed / 2 : speed;
        Vector3 pos = road.transform.position;
        pos.z += this.forwardSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.position = SteeringPosition(false);
        else if (Input.GetKey(KeyCode.RightArrow))
            this.transform.position = SteeringPosition(true);

        if (pos.z > 137.5f)
            pos.z = 0.0f;

        road.transform.position = pos;
    }

    private Vector3 SteeringPosition(bool steerRight)
    {
        Vector3 pos = this.transform.position;
        pos.x += this.steeringSpeed * Time.deltaTime * (steerRight ? -1 : 1);
        return pos;
    }
}
