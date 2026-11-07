using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Scanner : MonoBehaviour
{
    public List<GameObject> scannedItems;
    public TextMeshProUGUI scannerDisplay;
    public string scannerText;
    private void Awake()
    {
        ResetScanner();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Grocery"))
        {
            Debug.Log("Grocery Scanned!");
            if (scannedItems.Contains(collision.gameObject)) return;
            scannedItems.Add(collision.gameObject);
            scannerDisplay.text = scannerText + GetTotalScannedPrice();
        }
    }

    public List<GameObject> GetScannedItems()
    {
        return scannedItems;
    }

    public float GetTotalScannedPrice()
    {
        float totalPrice = 0f;
        for(int i = 0; i < scannedItems.Count; i++)
        {
            totalPrice += scannedItems[i].GetComponent<Grocery>().GetPrice();
        }
        return totalPrice;
    }

    [ContextMenu("Reset Scanner")]
    public void ResetScanner()
    {
        scannedItems = new List<GameObject>();
    }
}
