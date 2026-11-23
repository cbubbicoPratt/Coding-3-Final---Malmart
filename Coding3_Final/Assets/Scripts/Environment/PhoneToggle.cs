using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhoneToggle : MonoBehaviour
{
    //FINISHED: make "phone" button move menu panel, become unclickable
    //TODO: make clickable again when menu is gone

    //set this to the main canvas of the scene (maybe find a better mode of detection later
    public GameObject mainCanvas;
    public GameObject phoneMenu;
    private Transform pMenuStartPos;
    public int moveTime;
    public bool clickable;

    //event for if we need to check if the panel's on screen or not (fully finished its up/down animation)
    public UnityEvent<bool> panelOnScreen;
    private void Start()
    {
        clickable = true;
        pMenuStartPos = phoneMenu.transform;
    }

    private void Update()
    {
        if (clickable)
        {
            gameObject.GetComponent<Button>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Button>().enabled = false;
        }
    }

    public void MovePanelUp()
    {
        //if the panel moves up phone becomes unclickable
        clickable = false;
        StartCoroutine(PanelMove(pMenuStartPos.position, mainCanvas.transform.position));
        panelOnScreen.Invoke(true);
    }

    public void MovePanelDown()
    {
        //if the panel moves back down we can click the phone again
        clickable = true;
        StartCoroutine(PanelMove(mainCanvas.transform.position, pMenuStartPos.position));
        panelOnScreen.Invoke(false);
    }

    IEnumerator PanelMove(Vector3 startPos, Vector3 endPos)
    {
        for (float i = 0; i < moveTime; i += Time.deltaTime)
        {
            phoneMenu.transform.position = Vector3.Lerp(startPos, endPos, i * 5f);
            yield return null;
        }
        yield break;
    }
}
