using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider2D))]
public class Scanner : MonoBehaviour
{
    public UnityEvent onScan;
    public List<GameObject> scannedItems;

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
            onScan?.Invoke();
        }
    }

    public List<GameObject> GetScannedItems()
    {
        return scannedItems;
    }

    [ContextMenu("Reset Scanner")]
    public void ResetScanner()
    {
        scannedItems = new List<GameObject>();
    }
}
