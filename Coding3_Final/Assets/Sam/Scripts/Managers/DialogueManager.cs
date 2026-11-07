using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public Image background;
    public void ResetDialogue()
    {
        
    }
    public void StartDialogue(NPCData data)
    {
        Debug.Log("Dialogue Started!");
        dialogueRunner.name = data.NPCName;
        dialogueRunner.StartDialogue(data.NPCName);
    }
}
