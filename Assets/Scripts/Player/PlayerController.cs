using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Keymapping.keyBindings["MoveForward"]))
        {
            Debug.Log("W pressed");
        }
    }
}
