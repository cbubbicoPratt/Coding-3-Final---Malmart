using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Bag : MonoBehaviour
{
    [System.Serializable]
    public struct Events
    {
        public UnityEvent OnCompletion;
        public UnityEvent OnGroceryBagged;
    }

    public Events events;

    [System.Serializable]
    public struct References
    {
        public GroceryManager groceryManager;// => GameObject.FindGameObjectWithTag("GroceryManager").GetComponent<GroceryManager>();
        public Scanner scanner;// => GameObject.FindGameObjectWithTag("Scanner").GetComponent<Scanner>();
    }
    public References references;
    
    public List<GameObject> BaggedItems;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Grocery"))
        {
            Debug.Log("Grocery Found!");
            if (BaggedItems.Contains(collision.gameObject)) return;
            if (!references.scanner.GetScannedItems().Contains(collision.gameObject)) return;

            BaggedItems.Add(collision.gameObject);

            collision.gameObject.transform.SetParent(transform);

            events.OnGroceryBagged?.Invoke();

            if(collision.gameObject.GetComponent<Image>() != null)
            {
                collision.gameObject.GetComponent<Image>().enabled = false;
            }

            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            }

            CheckIfBagComplete();
        }
    }
    private float totalPrice;
    public void CheckIfBagComplete()
    {
        totalPrice = 0;
        for (int i = 0; i < BaggedItems.Count; i++) totalPrice += BaggedItems[i].GetComponent<Grocery>().GetPrice();
        if(totalPrice == references.groceryManager.GetTotalPrice())
        {
            Debug.Log("Bag Complete!");
            events.OnCompletion?.Invoke();
            references.groceryManager.CompleteNPC();
        }
    }

    public void ResetBag()
    {
        totalPrice = 0;
        BaggedItems = new List<GameObject>();
    }
}
