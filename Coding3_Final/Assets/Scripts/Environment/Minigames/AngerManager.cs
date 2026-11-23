using UnityEngine;
[RequireComponent(typeof(Timer))]
public class NPCAnger : MonoBehaviour
{
    public Timer timer => GetComponent<Timer>();

    public float angerAmount = 0f; //Ranges from 0 to 1, when hitting 1, npc gets angry

    public PlayerManager playerManager => FindFirstObjectByType<PlayerManager>();

    public InGameEvent currentNPC;

    public bool isAngry;

    
    private void Start()
    {
        isAngry = false;
        timer.onComplete.AddListener(OnAngry);
    }
    public void NewNPC(InGameEvent npcData)
    {
        currentNPC = npcData;
        ResetAnger();
    }

    public void ResetAnger()
    {
        if(currentNPC == null)
        {
            Debug.Log("There is no current NPC being saved at this time");
            return;
        }
        angerAmount = 0;
        timer.ResetTimer();
        timer.SetTimerLength(currentNPC.timeTillAngry);
        isAngry = false;
    }

    public void UpdateAnger(float angerAmount)
    {
        this.angerAmount = angerAmount;
        
    }

    public void OnAngry()
    {
        playerManager.DecreaseWage(currentNPC.angryDamagePerSecond / 60f);
    }
}
