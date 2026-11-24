using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScannerFlash : MonoBehaviour
{
    [SerializeField] private Image imageToFlash;
    [SerializeField] private Color colorToFlashTo;
    [SerializeField] private float flashTime;

    private bool flashing = false;
    private Color defaultColor;

    private void Start()
    {
        if (imageToFlash == null) return;
        defaultColor = imageToFlash.color;
    }

    public void Flash()
    {
        if (flashing) return;
        StartCoroutine(FlashAnim());
    }

    IEnumerator FlashAnim()
    {
        flashing = true;
        imageToFlash.color = colorToFlashTo;
        yield return new WaitForSeconds(flashTime);
        imageToFlash.color = defaultColor;
        flashing = false;
    }
}
