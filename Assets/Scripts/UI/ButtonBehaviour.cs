using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour
{
    public static void DeselectButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
