using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightTrigger : MonoBehaviour
{
    public UnityEvent<bool> rightTriggered;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Moving Right");
        rightTriggered.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Stopped Moving Right");
        rightTriggered.Invoke(false);
    }
}
