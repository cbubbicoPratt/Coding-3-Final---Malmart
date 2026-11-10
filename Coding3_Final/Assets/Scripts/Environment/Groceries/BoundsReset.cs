using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class BoundsReset : MonoBehaviour
{
    [SerializeField] private Transform resetPos;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Grocery"))
        {
            collision.transform.position = resetPos.position;
        }
    }
}
