using UnityEngine;

public class ReadTimer : MonoBehaviour
{
    [SerializeField] Material currentMaterial;
    [SerializeField] private string parameterID;

    public Color color = Color.white;
    public void SetMaterialValue(float newValue)
    {
        currentMaterial.SetFloat(parameterID, newValue);
    }

    public void SetColor(Color col)
    {
        currentMaterial.SetColor("_BaseColor", col);
    }
}
