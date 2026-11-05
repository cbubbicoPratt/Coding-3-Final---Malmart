using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class Moveable : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool isDragging = false;
    public bool isMovable = true;
    private Vector2 offset;

    [System.Serializable]
    public struct OnClickEvents
    {
        public UnityEvent onMouseDown;
        public UnityEvent onMouseRelease;
    }
    [Header("Events")]
    public OnClickEvents onClickEvents;
    public void OnDrag(PointerEventData eventData)
    {
        if (!isMovable) return;
        transform.position = eventData.position - offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isMovable) return;
        offset = eventData.position - new Vector2(transform.position.x, transform.position.y);
        isDragging = true;
        transform.SetAsLastSibling();
        onClickEvents.onMouseDown?.Invoke();

        if (gameObject.GetComponent<Image>() != null)
        {
            gameObject.GetComponent<Image>().raycastTarget = false;
        }

        if (gameObject.GetComponent<Collider2D>() != null)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
        {
            if (!isMovable) return;
            isDragging = false;
            onClickEvents.onMouseRelease?.Invoke();

            if (gameObject.GetComponent<Image>() != null)
            {
                gameObject.GetComponent<Image>().raycastTarget = true;
            }


            if (gameObject.GetComponent<Collider2D>() != null)
            {
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
