using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class ScannerText : MonoBehaviour
{
    public string additionalText = "";
    public TextMeshProUGUI text;

    public Scanner scanner => GameObject.FindFirstObjectByType<Scanner>();

    public void AddTotal()
    {
        List<GameObject> scannedItems = scanner.GetScannedItems();

        float total = 0f;

        for(int i = 0; i < scannedItems.Count; i++)
        {
            total += scannedItems[i].GetComponent<Grocery>().GetPrice();
        }

        ChangeText(additionalText + total);
    }
    public void ChangeText(string newText)
    {
        text.text = newText;
    }
}
