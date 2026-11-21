using UnityEngine.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LeftTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent<bool> leftTriggered;
    public CameraPan camPan;

    private void Awake()
    {
        camPan = GameObject.FindAnyObjectByType<CameraPan>();
        camPan.maxLeft.AddListener(DisableTrigger);
    }

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
