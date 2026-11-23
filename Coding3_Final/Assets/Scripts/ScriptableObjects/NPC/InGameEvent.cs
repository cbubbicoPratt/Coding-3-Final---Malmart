using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum EventType
{
    NPC,
    Conditional,
    Wait
}
public enum NPCType
{
    Grocery,
    JustDialogue,
    Phone,
    Legislature,
    Bomb,
}
[System.Serializable]
public struct GroceryOrder
{
    public GroceryData groceryType;
    public int amount;
}
[System.Serializable]
public struct LegislatureOrder
{
    public LegislaturePiece legislaturePiece;
    public int amount;
}
[System.Serializable]
public enum PhoneTarget
{
    FoodAisle,
    CarAisle,

}
[CreateAssetMenu(fileName = "InGameEvent", menuName = "Scriptable Objects/InGameEvent")]
public class InGameEvent : ScriptableObject
{
    
    //Type of event
    public EventType eventType;

    //CONDITIONAL EVENT
    public UnityEvent ToComplete;

    //WAIT EVENT
    public float waitTime;

    //NPC STUFF
    public NPCType npcType;
    public string NPCName;
    public Sprite NPCImage;
    public List<GroceryOrder> groceryList;

    public List<LegislatureOrder> legislatureStack;

    public PhoneTarget phoneTarget;

    public int BombPressCount;
    public UnityEvent OnComplete;

    //DIALOGUE STUFF
    public string NPCFirstNodeName;

    //PUNISHMENT STUFF
    public float timeTillAngry;
    public float angryDamagePerSecond;



}
