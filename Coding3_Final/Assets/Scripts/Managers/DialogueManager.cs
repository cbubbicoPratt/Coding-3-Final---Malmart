using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public Image backgroundDisplay;
    public TextMeshProUGUI nameDisplay;
    public TextMeshProUGUI textDisplay;

    public GroceryManager groceryManager;

    private InGameEvent currentNPC;
    public void ResetDialogue()
    {
        if(currentNPC != null)
        {
            StartDialogue(currentNPC);
        }
    }
    public void StartDialogue(InGameEvent data)
    {
        Debug.Log("Dialogue Started!");
        backgroundDisplay.enabled = true; 
        nameDisplay.enabled = true;
        textDisplay.enabled = true;

        nameDisplay.text = data.NPCName;
        currentNPC = data;
        dialogueRunner.Stop();
        dialogueRunner.StartDialogue(data.NPCFirstNodeName);
    }

    public void DialogueComplete()
    {
        groceryManager.CompleteDialogue();
    }
}
