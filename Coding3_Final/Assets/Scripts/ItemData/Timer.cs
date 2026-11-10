using UnityEngine;
using UnityEngine.Events;
public class Timer : MonoBehaviour
{
    public UnityEvent onComplete;
    public UnityEvent<float> counting;
    private bool isCounting = false;
    [SerializeField] private float TimerLength = 1f;


    private float counter = 0;
    void Update()
    {
        if (isCounting)
        {
            counter += Time.deltaTime;

            counting?.Invoke(counter / TimerLength);
        }

        if (counter >= TimerLength)
        {
            onComplete?.Invoke();
            PauseTimer();
            ResetTimer();
        }
    }

    public void PauseTimer()
    {
        isCounting = false;
    }

    public void StartTimer()
    {
        isCounting = true;
    }

    public void ResetTimer()
    {
        counter = 0;
    }
    public void SetTimerLength(float newLength)
    {
        TimerLength = newLength;
    }

    public void GetCompletion()
    {

    }
}
