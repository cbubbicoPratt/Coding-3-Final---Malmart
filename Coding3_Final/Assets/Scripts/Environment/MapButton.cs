using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    //TODO: attach PhoneButton SO to this class to broadcast an event with its name
    //TODO: call PhoneToggle MovePanelDown() method with this event
    public PhoneToggle pToggle;
    public PhoneButton buttonData;

    public UnityEvent<string> clicked;
    private void Start()
    {
        pToggle = FindAnyObjectByType<PhoneToggle>();
        pToggle.panelOnScreen.AddListener(CheckClickable);
        gameObject.GetComponent<Button>().onClick.AddListener(MapButtonClicked);
    }

    public void CheckClickable(bool isClickable)
    {
        gameObject.GetComponent<Button>().enabled = isClickable;
    }

    public void MapButtonClicked()
    {
        clicked.Invoke(buttonData.name);
        Debug.Log("clicked " + buttonData.name);
    }
}
