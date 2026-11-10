using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

        public Timer failStateTimer;

        public GameManager gameManager;

        public DialogueManager dialogueManager;
    }
    public References references;

    [HideInInspector] public List<GameObject> activeGroceries;

    public float totalPrice = 0;

    public void NewNPC(NPCData newNPC)
    {
        ResetMinigame();
        List<GroceryOrder> groceryList = newNPC.groceryList;

        references.failStateTimer.ResetTimer();
        references.failStateTimer.SetTimerLength(newNPC.timeToComplete);
        references.failStateTimer.StartTimer();

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
        events.OnNewNPC?.Invoke();
    }

    [ContextMenu("Reset Minigame")]
    public void ResetMinigame()
    {
        references.scanner.ResetScanner();
        references.bag.ResetBag();
        references.failStateTimer.PauseTimer();
        references.failStateTimer.ResetTimer();

        for(int i = 0; i < activeGroceries.Count; i++)
        {
            Destroy(activeGroceries[i]);

        }

        activeGroceries = new List<GameObject>();
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
        references.gameManager.NPCCompleted();
        ResetMinigame();
    }

    public void OnFail()
    {
        Debug.Log("Failed NPC!");
        events.OnFail?.Invoke();
        ResetMinigame();
    }
}
