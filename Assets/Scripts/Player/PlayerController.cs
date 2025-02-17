using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float steeringSpeed = 1.0f;
    [SerializeField] GameObject road;
    float forwardSpeed;

    // Update is called once per frame
    void Update()
    {

        this.forwardSpeed = Input.GetKey(KeyCode.UpArrow) ? 500.0f : 200.0f;
        Vector3 pos = road.transform.position;
        pos.z += this.forwardSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
            this.transform.position = SteeringPosition(false);
        else if (Input.GetKey(KeyCode.RightArrow))
            this.transform.position = SteeringPosition(true);

        if (pos.z > 75.0f)
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
