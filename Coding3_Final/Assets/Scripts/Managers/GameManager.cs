using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct GroceryGame
    {
        public List<InGameEvent> GroceryTimeline;
        public GroceryManager groceryManager;
        public int eventIndex;
    }
    [Header("Grocery Game")]
    public GroceryGame groceryGame;

    [System.Serializable]
    public struct PhoneGame
    {
        public List<InGameEvent> PhoneTimeline;
        public int eventIndex;
    }
    [Header("Phone Game")]
    public PhoneGame phoneGame;

    [System.Serializable]
    public struct LegislatureGame
    {
        public List<InGameEvent> LegislatureTimeline;
        public int eventIndex;
    }
    [Header("Legislature Game")]
    public LegislatureGame legislatureGame;

    [System.Serializable]
    public struct BombGame
    {
        public List<InGameEvent> BombTimeline;
        public int eventIndex;
    }
    [Header("Legislature Game")]
    public BombGame bombGame;

    [System.Serializable]

    public struct Events
    {
        public UnityEvent OnGameWin;
        
        public UnityEvent OnGameLose;
    }

    public Events events;

    public PlayerManager playerManager => GameObject.FindAnyObjectByType<PlayerManager>();
    
    private void Awake()
    {
        groceryGame.eventIndex = 0;
        phoneGame.eventIndex = 0;
        legislatureGame.eventIndex = 0;
        bombGame.eventIndex = 0;

    }
    private void Start()
    {
        SendNPC(NPCType.Grocery);
    }
    public void NextNPC(NPCType nextType)
    {
        //based on NPC type, cue next part.
        switch (nextType)
        {
            case NPCType.Grocery:
                groceryGame.eventIndex++;
                break;
            case NPCType.Phone:
                phoneGame.eventIndex++;
                break;
            case NPCType.Legislature:
                legislatureGame.eventIndex++;
                break;
            case NPCType.Bomb:
                bombGame.eventIndex++;
                break;
        }
    }
    public void SendNPC(NPCType nextType)
    {
        switch (nextType)
        {
            case NPCType.Grocery:
                groceryGame.groceryManager.NewNPC(groceryGame.GroceryTimeline[groceryGame.eventIndex]);
                break;
            case NPCType.Phone:
                break;
            case NPCType.Legislature:
                break;
            case NPCType.Bomb:
                break;
        }
    }

    public void NPCCompleted(NPCType completedType)
    {
        switch (completedType)
        {
            case NPCType.Grocery:
                groceryGame.GroceryTimeline[groceryGame.eventIndex].OnComplete?.Invoke();
                NextNPC(NPCType.Grocery);
                SendNPC(NPCType.Grocery);
                break;
            case NPCType.Phone:
                break;
            case NPCType.Legislature:
                break;
            case NPCType.Bomb:
                break;
        }
    }

    public void InduceFail()
    {
        events.OnGameLose?.Invoke();
    }
}
