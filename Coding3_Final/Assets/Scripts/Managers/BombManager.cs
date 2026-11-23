using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BombManager : MonoBehaviour
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
        public DialogueManager dialogueManager;

    }
    public References references;

    public int targetPresses;
    private int pressCount;

    public void NewNPC(InGameEvent newNPC)
    {
        ResetMinigame();
        references.angerManager.NewNPC(newNPC);
        targetPresses = newNPC.BombPressCount;
        //references.dialogueManager.StartDialogue(newNPC);
        events.OnNewNPC?.Invoke();
    }


    [ContextMenu("Reset Minigame")]
    public void ResetMinigame()
    {
        references.angerManager.ResetAnger();
        //references.dialogueManager.dialogueRunner.Stop();
        pressCount = 0;
    }
    public void PressButton()
    {
        pressCount++;
        if(pressCount >= targetPresses)
        {
            CompleteNPC();
        }
    }
    public void CompleteNPC()
    {
        Debug.Log("Won NPC!");
        events.OnCompletion?.Invoke();
        ResetMinigame();
        references.gameManager.NPCCompleted(NPCType.Bomb);
    }
}
