using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;
public class DialogueTimer : MonoBehaviour
{
    public UnityEvent OnOptionsLose;
    public UnityEvent OnContinue;

    public float timerLength = 1f;


    public enum TimerType
    {
        CountdownContinues,
        CountdownFails
    }

    public TimerType type = TimerType.CountdownContinues;

    public Timer timer;

    public ReadTimer display;

    public DialogueRunner runner;

    [YarnCommand("Change_Type")]
    public void ChangeTimerType(float time, string type)
    {
        switch (type)
        {
            case "CountdownContinues":
                display.SetColor(Color.white);
                this.type = TimerType.CountdownContinues;
                break;
            case "CountdownFails":
                display.SetColor(Color.red);
                this.type = TimerType.CountdownFails;
                break;
        }

        timerLength = time;
    }
    [YarnCommand("NewLine")]
    public void NewLine()
    {
        timer.ResetTimer();
        timer.SetTimerLength(timerLength);
        timer.StartTimer();
    }

    public void OnTimerEnd()
    {
        switch (type)
        {
            case TimerType.CountdownContinues:
                OnContinue?.Invoke();
                runner.RequestNextLine();
                break;
            case TimerType.CountdownFails:
                OnOptionsLose?.Invoke();
                break;
        }
    }

}
