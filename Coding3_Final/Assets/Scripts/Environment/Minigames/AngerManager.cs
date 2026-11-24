using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Timer))]
public class NPCAnger : MonoBehaviour
{
    public Timer timer => GetComponent<Timer>();

    public float angerAmount = 0f; //Ranges from 0 to 1, when hitting 1, npc gets angry

    public PlayerManager playerManager => FindFirstObjectByType<PlayerManager>();

    public InGameEvent currentNPC;

    public UnityEvent<bool> OnTurnAngry;
    public UnityEvent<float> OnAngerUpdate;
    public bool isAngry;

    
    private void Start()
    {
        isAngry = false;
        timer.onComplete.AddListener(OnAngry);
        timer.counting.AddListener(UpdateAnger);
    }
    public void NewNPC(InGameEvent npcData)
    {
        currentNPC = npcData;
        ResetAnger();
        timer.StartTimer();
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
        timer.PauseTimer();
        timer.SetTimerLength(currentNPC.timeTillAngry);
        isAngry = false;
        OnTurnAngry?.Invoke(false);
        OnAngerUpdate?.Invoke(0);
    }

    public void UpdateAnger(float angerAmount)
    {
        this.angerAmount = angerAmount;
        OnAngerUpdate?.Invoke(angerAmount);
    }

    public void OnAngry()
    {
        OnTurnAngry?.Invoke(true);
        playerManager.DecreaseWage(currentNPC.angryDamagePerSecond / 60f);
    }
}
