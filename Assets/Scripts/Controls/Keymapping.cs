using System.Collections.Generic;
using UnityEngine;

public class Keymapping : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keyBindings;

    void Awake()
    {
        keyBindings = new Dictionary<string, KeyCode>
        {
            {"MoveLeft", KeyCode.A },
            {"MoveRight", KeyCode.D },
            {"MoveForward", KeyCode.W },
            {"MoveBackwards", KeyCode.S},
            {"Pause", KeyCode.Escape }
        };
    }
}
