using UnityEngine;
using UnityEngine.Events;
using System;
public class PlayerManager : MonoBehaviour
{
    public float playerWage;

    private float maxWage;

    public GameManager gameManager => FindAnyObjectByType<GameManager>();

    [System.Serializable]
    public struct Events
    {
        public UnityEvent OnPlayerLose;
        public UnityEvent OnWageIncrease;
        public UnityEvent OnWageDecrease;

        public UnityEvent OnMaxWageIncrease;
        public UnityEvent OnMaxWageDecrease;
    }
    public Events events;

    public float GetWage()
    {
        return playerWage;
    }
    public void SetWage(float newWage)
    {
        if(newWage <= maxWage)
        {
            this.playerWage = newWage;
        }
        else
        {
            playerWage = maxWage;
        }

        if(playerWage <= 0)
        {
            gameManager.InduceFail();
            events.OnPlayerLose?.Invoke();
        }
    }

    public void DecreaseWage(float decreaseAmount)
    {
        float newWage = playerWage - decreaseAmount;

        if (newWage <= maxWage)
        {
            this.playerWage = newWage;
        }
        else
        {
            playerWage = maxWage;
        }
        events.OnWageDecrease?.Invoke();

        if (playerWage <= 0)
        {
            gameManager.InduceFail();
            events.OnPlayerLose?.Invoke();
        }
    }

    public void IncreaseWage(float increaseAmount)
    {
        float newWage = playerWage + increaseAmount;

        if (newWage <= maxWage)
        {
            this.playerWage = newWage;
        }
        else
        {
            playerWage = maxWage;
        }

        events.OnWageIncrease?.Invoke();

        if (playerWage <= 0)
        {
            gameManager.InduceFail();
            events.OnPlayerLose?.Invoke();
        }
    }

    public void SetMaxWage(float newMaxWage)
    {
        if(newMaxWage > maxWage)
        {
            events.OnMaxWageIncrease?.Invoke();
        }else if(newMaxWage < maxWage)
        {
            events.OnMaxWageDecrease?.Invoke();
        }

        maxWage = newMaxWage;
    }

    
}
