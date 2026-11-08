using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
[System.Serializable]
public struct GroceryOrder
{
    public GroceryData groceryType;
    public int amount;
}
[CreateAssetMenu(fileName = "NPCData", menuName = "Scriptable Objects/NPCData")]
public class NPCData : ScriptableObject
{

    public string NPCName;
    public List<GroceryOrder> groceryList;

    public string NPCFirstNodeName;
    public float timeToComplete;
    public float timeOnCooldown;
}
