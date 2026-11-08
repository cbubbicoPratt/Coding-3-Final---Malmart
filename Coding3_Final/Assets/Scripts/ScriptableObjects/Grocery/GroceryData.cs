using UnityEngine;

[CreateAssetMenu(fileName = "GroceryData", menuName = "Scriptable Objects/GroceryData")]
[System.Serializable]
public class GroceryData : ScriptableObject
{
    public string groceryName;
    public float price;
    public Sprite image;
}
