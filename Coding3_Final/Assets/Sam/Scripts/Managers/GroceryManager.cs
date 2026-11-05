using System.Collections.Generic;
using UnityEngine;

public class GroceryManager : MonoBehaviour
{
    [System.Serializable]
    public struct References
    {
        public Transform spawnLocation;
        public Transform CanvasToParent;
    }
    public References references;

    [System.Serializable]
    public struct Groceries
    {
        public int amount;
        public GroceryData type;

    }
    public List<Groceries> groceryList;

    [ContextMenu("New NPC")]
    public void NewNPC()
    {
        for(int i = 0; i < groceryList.Count; i++)
        {
            for (int amount = 0; amount < groceryList[i].amount; amount++)
            {
                GameObject newGrocery = new GameObject();
                newGrocery.transform.parent = references.CanvasToParent;
                newGrocery.transform.position = references.spawnLocation.position;
                newGrocery.transform.rotation = Quaternion.identity;

                Grocery GrocerySetup = newGrocery.AddComponent<Grocery>();
                GrocerySetup.SetGroceryData(groceryList[i].type);
                GrocerySetup.SetupGrocery();

                newGrocery.transform.name = GrocerySetup.GetName() + (amount + 1).ToString();
            }
        }
    }
}
