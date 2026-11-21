using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightTrigger : MonoBehaviour
{
    public UnityEvent<bool> rightTriggered;

    public void OnMouseEnter()
    {
        Debug.Log("Moving Right");
        rightTriggered.Invoke(true);
    }

    public void OnMouseExit()
    {
        Debug.Log("Stopped Moving Right");
        rightTriggered.Invoke(false);
    }
}
