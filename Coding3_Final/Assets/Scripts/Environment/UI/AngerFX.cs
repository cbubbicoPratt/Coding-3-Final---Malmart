using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AngerFX : MonoBehaviour
{
    [SerializeField] private Image TaskImage;
    [SerializeField] private Color colorToTurn;

    [SerializeField] private float shakingIntensity;
    [SerializeField] private float shakeSpeed;
    private Vector3 Origin;
    private Vector3 Offset;
    private float AngerAmount;
    private Color defaultColor;

    private bool isShaking;

    private void Start()
    {
        if (TaskImage == null) return;
        defaultColor = TaskImage.color;
        Origin = transform.position;
        Offset = Vector3.zero;
    }

    public void SetShaking(bool isShaking)
    {
        if(!this.isShaking && isShaking)
        {
            this.isShaking = true;
            StartCoroutine(Shaking());
        }else if(this.isShaking && !isShaking)
        {
            this.isShaking = false;
            StopAllCoroutines();
        }
    }

    public void SetAngerAmount(float newAmount)
    {
        AngerAmount = Mathf.Clamp(newAmount, 0, 1);
        TaskImage.color = Color.Lerp(defaultColor, colorToTurn, AngerAmount);
    }

    IEnumerator Shaking()
    {
        while (isShaking)
        {
            Origin = transform.position - Offset;
            Offset = Random.insideUnitCircle * shakingIntensity;
            transform.position = Origin + Offset;
            yield return new WaitForSeconds(shakeSpeed / 60f);
        }
    }
}
