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

    public void ResetDialogue()
    {
        
    }
    public void StartDialogue(NPCData data)
    {
        Debug.Log("Dialogue Started!");
        backgroundDisplay.enabled = true;
        nameDisplay.enabled = true;
        textDisplay.enabled = true;

        nameDisplay.text = data.NPCName;
        dialogueRunner.StartDialogue(data.NPCFirstNodeName);
    }
}
