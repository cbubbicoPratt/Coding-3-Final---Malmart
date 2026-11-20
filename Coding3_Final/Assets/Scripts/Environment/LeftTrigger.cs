using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftTrigger : MonoBehaviour
{
    public UnityEvent<bool> leftTriggered;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Moving Left");
        leftTriggered.Invoke(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Stopped Moving Left");
        leftTriggered.Invoke(false);
    }
}
