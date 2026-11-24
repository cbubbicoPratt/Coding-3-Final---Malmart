using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LegislatureManager : MonoBehaviour
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
        public GameManager gameManager;
        public NPCAnger angerManager;

        public Transform SpawnPoint;
        public Transform canvasToParent;
    }
    public References references;

    public List<GameObject> activeLegislature;

    private bool MinigameEnabled;
    public void NewNPC(InGameEvent newNPC)
    {
        Debug.Log("Legislature Started!");
        ResetMinigame();
        references.angerManager.NewNPC(newNPC);

        List<LegislatureOrder> newStack = newNPC.legislatureStack;
        MinigameEnabled = true;
        for(int i = 0; i < newStack.Count; i++)
        {
            for(int amount = 0; amount < newStack[i].amount; amount++)
            {
                GameObject newPaper = new GameObject();
                newPaper.transform.parent = references.canvasToParent;
                newPaper.transform.position = references.SpawnPoint.position;
                newPaper.transform.rotation = Quaternion.identity;

                newPaper.transform.name = newStack[i].legislaturePiece.name + (amount + 1).ToString();
                Image newImage = newPaper.AddComponent<Image>();
                newImage.sprite = newStack[i].legislaturePiece.image;
                newImage.raycastTarget = false;
                activeLegislature.Add(newPaper);
            }
        }

        events.OnNewNPC?.Invoke();
    }

    [ContextMenu("Reset Minigame")]
    public void ResetMinigame()
    {
        references.angerManager.ResetAnger();
    }

    public void OnClick()
    {
        if (!MinigameEnabled) return;
        if(activeLegislature.Count > 0)
        {
            GameObject activePiece = activeLegislature[activeLegislature.Count - 1];
            activeLegislature.Remove(activePiece);
            GameObject.Destroy(activePiece);
        }

        if(activeLegislature.Count <= 0)
        {
            CompleteNPC();
        }
    }
    public void CompleteNPC()
    {
        Debug.Log("Won NPC!");
        events.OnCompletion?.Invoke();
        ResetMinigame();
        references.gameManager.NPCCompleted(NPCType.Legislature);
    }
}
