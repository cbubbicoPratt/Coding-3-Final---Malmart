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
        public LegislatureManager legislatureManager;
        public int eventIndex;
    }
    [Header("Legislature Game")]
    public LegislatureGame legislatureGame;

    [System.Serializable]
    public struct BombGame
    {
        public List<InGameEvent> BombTimeline;

        public BombManager bombManager;
        public int eventIndex;
    }
    [Header("Bomb Game")]
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
    public void SendNPC(NPCType from)
    {

        switch (from)
        {
            case NPCType.Grocery:
                RouteEvent(groceryGame.GroceryTimeline[groceryGame.eventIndex], from);
                break;
            case NPCType.Phone:
                RouteEvent(phoneGame.PhoneTimeline[phoneGame.eventIndex], from);
                break;
            case NPCType.Legislature:
                RouteEvent(legislatureGame.LegislatureTimeline[legislatureGame.eventIndex], from);
                break;
            case NPCType.Bomb:
                RouteEvent(bombGame.BombTimeline[bombGame.eventIndex], from);
                break;
        }
    }

    private void RouteEvent(InGameEvent current, NPCType target)
    {
        switch (current.eventType)
        {
            case EventType.Wait:
                CreateWaiter(current);
                break;
            case EventType.Conditional:
                CreateConditional(current);
                break;
            case EventType.NPC:
                switch (current.npcType)
                {
                    case NPCType.Grocery:
                        groceryGame.groceryManager.NewNPC(current);
                        break;
                    case NPCType.Phone:
                        break;
                    case NPCType.Legislature:
                        legislatureGame.legislatureManager.NewNPC(current);
                        break;
                    case NPCType.Bomb:
                        bombGame.bombManager.NewNPC(current);
                        break;
                }
                break;
        }
    }

    public void CreateWaiter(InGameEvent current)
    {
        GameObject waiter = new GameObject();
        Timer waitTimer = waiter.AddComponent<Timer>();
        waitTimer.SetTimerLength(current.waitTime);
        waitTimer.onComplete.AddListener(() => NPCCompleted(current.npcType));
        waitTimer.onComplete.AddListener(() => GameObject.Destroy(waiter));

        waitTimer.ResetTimer(); 
        waitTimer.StartTimer();
    }

    public void CreateConditional(InGameEvent current)
    {

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
                legislatureGame.LegislatureTimeline[legislatureGame.eventIndex].OnComplete?.Invoke();
                NextNPC(NPCType.Legislature);
                SendNPC(NPCType.Legislature);
                break;
            case NPCType.Bomb:
                bombGame.BombTimeline[bombGame.eventIndex].OnComplete?.Invoke();
                NextNPC(NPCType.Bomb);
                SendNPC(NPCType.Bomb);
                break;
        }
    }

    public void InduceFail()
    {
        events.OnGameLose?.Invoke();
    }
}
