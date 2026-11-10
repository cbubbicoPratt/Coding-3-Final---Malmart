using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct GroceryGame
    {
        public List<NPCData> npcs;
        public GroceryManager groceryManager;

        public Timer CooldownTimer;
    }
    public GroceryGame groceryGame;

    [System.Serializable]

    public struct Events
    {
        public UnityEvent OnGameWin;
        
        public UnityEvent OnGameLose;
    }

    public Events events;

    private int NPCIndex = 0;

    private void Awake()
    {
        NPCIndex = 0;

    }
    private void Start()
    {
        SendNPC();
    }
    public void NextNPC()
    {
        NPCIndex++;
        if(NPCIndex >= groceryGame.npcs.Count)
        {
            events.OnGameWin?.Invoke();
            Debug.Log("Game Won!");
        }
    }
    public void SendNPC()
    {
        groceryGame.groceryManager.NewNPC(groceryGame.npcs[NPCIndex]);
    }

    public void NPCCompleted()
    {
        groceryGame.CooldownTimer.ResetTimer();
        groceryGame.CooldownTimer.SetTimerLength(groceryGame.npcs[NPCIndex].timeOnCooldown);
        groceryGame.CooldownTimer.StartTimer();
        NextNPC();
    }

    public void InduceFail()
    {
        events.OnGameLose?.Invoke();
    }
}
