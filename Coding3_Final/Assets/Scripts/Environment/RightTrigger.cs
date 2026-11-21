using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RightTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent<bool> rightTriggered;
    public CameraPan camPan;

    private void Awake()
    {
        camPan = GameObject.FindAnyObjectByType<CameraPan>();
        camPan.maxRight.AddListener(DisableTrigger);
    }

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

    public void DisableTrigger(bool maxTriggered)
    {
        if (maxTriggered)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Image>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<Image>().enabled = true;
        }
    }
}
