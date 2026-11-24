using TMPro;
using UnityEngine;

public class WageReader : MonoBehaviour
{
    public string AdditionalText;
    public TextMeshProUGUI textDisplay;

    public PlayerManager playerManager => GameObject.FindAnyObjectByType<PlayerManager>();
    public void Update()
    {
        textDisplay.text = AdditionalText + playerManager.GetWage().ToString();
    }
}
