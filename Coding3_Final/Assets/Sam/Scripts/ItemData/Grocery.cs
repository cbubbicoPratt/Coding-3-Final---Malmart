using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Moveable))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Grocery : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Image image;

    [SerializeField] private RectTransform rTransform;

    [Header("")]
    [Header("GroceryData")]
    
    [SerializeField] private GroceryData data;

    [ContextMenu("Setup GameObject")]
    public void SetupGrocery()
    {
        rTransform = GetComponent<RectTransform>();
        col = gameObject.GetComponent<BoxCollider2D>();

        col.size = new Vector2(rTransform.rect.width, rTransform.rect.height);
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 100;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        image = gameObject.GetComponent<Image>();

        if(data == null)
        {
            Debug.Log("ERROR: DATA IS NOT SET FOR THIS GROCERY");
            return;
        }
        image.sprite = data.image;
    }

    public void SetGroceryData(GroceryData groceryData)
    {
        data = groceryData;
    }
    public float GetPrice()
    {
        return data.price;
    }

    public Sprite GetSprite()
    {
        return data.image;
    }

    public string GetName()
    {
        return data.groceryName;
    }
}
