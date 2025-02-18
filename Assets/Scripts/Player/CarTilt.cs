using UnityEngine;

public class CarTilt : MonoBehaviour
{
    private enum Direction { LEFT, RIGHT, CENTER }
    private Direction dir;
    private Direction lastDir;
    [SerializeField] GameObject player;
    [SerializeField] GameObject leftWheel, rightWheel;
    [SerializeField] float turn = 3;
    [SerializeField] float tilt = 3;
    float maxSpeed, currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        lastDir = dir;
        maxSpeed = player.GetComponent<PlayerController>().maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeed = player.GetComponent<PlayerController>().forwardSpeed;

        if (player.GetComponent<PlayerController>().steerRight)
        {
            dir = Direction.RIGHT;
        }
        else if (player.GetComponent<PlayerController>().steerLeft)
        {
            dir = Direction.LEFT;
        }
        else
        {
            dir = Direction.CENTER;
        }
    }

    private void FixedUpdate()
    {
        if (lastDir != dir)
        {
            Tilt(dir);
            lastDir = dir;
        }
    }

    private void Tilt(Direction dir)
    {
        Vector3 eulerCar = player.transform.eulerAngles;
        Vector3 eulerWheels = leftWheel.transform.eulerAngles;

        if (dir == Direction.LEFT)
        {
            eulerCar.y = -turn * (maxSpeed / currentSpeed);
            eulerCar.z = -tilt * (maxSpeed / currentSpeed);

            eulerWheels.y = -12 * (maxSpeed / currentSpeed) + 180f;
        }
        else if (dir == Direction.RIGHT)
        {
            eulerCar.y = turn * (maxSpeed / currentSpeed);
            eulerCar.z = tilt * (maxSpeed / currentSpeed);

            eulerWheels.y = 12 * (maxSpeed / currentSpeed) + 180f;
        }
        else
        {
            eulerCar.y = 0;
            eulerCar.z = 0;

            eulerWheels.y = 0 + 180f;
        }

        // Tween rotation

        // Car
        LeanTween.rotate(player, eulerCar, 0.2f);

        // Wheels
        LeanTween.rotate(leftWheel, eulerWheels, 0.2f);
        LeanTween.rotate(rightWheel, eulerWheels, 0.2f);
    }
}
