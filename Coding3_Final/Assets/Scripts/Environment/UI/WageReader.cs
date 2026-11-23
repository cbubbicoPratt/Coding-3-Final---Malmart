using TMPro;
using UnityEngine;

public class WageReader : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    public PlayerManager playerManager => GameObject.FindAnyObjectByType<PlayerManager>();
    public void Update()
    {
        textDisplay.text = playerManager.GetWage().ToString();
    }
}
