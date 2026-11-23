using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
public class GroceryManager : MonoBehaviour
{
    [System.Serializable]
    public struct Events
    {
        public UnityEvent OnNewNPC;
        public UnityEvent OnCompletion;
        public UnityEvent OnFail;
    }
    public Events events;

    [System.Serializable]
    public struct References
    {
        public Transform spawnLocation;
        public Transform CanvasToParent;
        public Bag bag;

        public Scanner scanner;

        public GameManager gameManager;

        public DialogueManager dialogueManager;

        public Image characterImage;
    }
    public References references;

    public List<GameObject> activeGroceries;

    public float totalPrice = 0;


    public bool DialogueCompleted;
    public bool GroceriesCompleted;

    public void NewNPC(InGameEvent newNPC)
    {
        ResetMinigame();
        List<GroceryOrder> groceryList = newNPC.groceryList;
        references.characterImage.sprite = newNPC.NPCImage;
        references.dialogueManager.StartDialogue(newNPC);
        totalPrice = 0;
        

        for (int i = 0; i < groceryList.Count; i++)
        {
            for (int amount = 0; amount < groceryList[i].amount; amount++)
            {
                GameObject newGrocery = new GameObject();
                newGrocery.transform.parent = references.CanvasToParent;
                newGrocery.transform.position = references.spawnLocation.position;
                newGrocery.transform.rotation = Quaternion.identity;

                Grocery GrocerySetup = newGrocery.AddComponent<Grocery>();
                GrocerySetup.SetGroceryData(groceryList[i].groceryType);
                GrocerySetup.SetupGrocery();

                newGrocery.transform.name = GrocerySetup.GetName() + (amount + 1).ToString();
                newGrocery.transform.tag = "Grocery";
                activeGroceries.Add(newGrocery);
                totalPrice += GrocerySetup.GetPrice();
            }
        }
        Debug.Log("Groceries Started!");
        events.OnNewNPC?.Invoke();
    }

    [ContextMenu("Reset Minigame")]
    public void ResetMinigame()
    {
        references.scanner.ResetScanner();
        references.bag.ResetBag();
        DialogueCompleted = false;
        GroceriesCompleted = false;

        for(int i = 0; i < activeGroceries.Count; i++)
        {
            Destroy(activeGroceries[i]);
        }

        activeGroceries = new List<GameObject>();
    }

    public void CompleteDialogue()
    {
        DialogueCompleted = true;
        Debug.Log("Dialogue Completed!");
        if (DialogueCompleted && GroceriesCompleted)
        {
            CompleteNPC();
        }
    }
    public void CompleteGroceries()
    {
        GroceriesCompleted = true;
        Debug.Log("Groceries Completed!");
        if(DialogueCompleted && GroceriesCompleted)
        {
            CompleteNPC();
        }
    }

    public List<GameObject> GetActiveGroceries()
    {
        return activeGroceries;
    }

    public float GetTotalPrice()
    {
        return totalPrice;
    }
    public void CompleteNPC()
    {
        Debug.Log("Won NPC!");
        events.OnCompletion?.Invoke();
        ResetMinigame();
        references.gameManager.NPCCompleted(NPCType.Grocery);
    }
}
