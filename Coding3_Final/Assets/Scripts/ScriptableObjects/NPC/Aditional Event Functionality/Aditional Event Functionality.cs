using UnityEngine;
public class AditionalEventFunctionality : MonoBehaviour
{
    public static AditionalEventFunctionality instance;

    public void TestFunction()
    {
        Debug.Log("It worked!");
    }

    public void SetMaxWage(float maxWage)
    {
        PlayerManager pm = FindAnyObjectByType<PlayerManager>();
        if (pm == null) return;

        pm.SetMaxWage(maxWage);
    }

    public void SetWage(float wage)
    {
        PlayerManager pm = FindAnyObjectByType<PlayerManager>();
        if (pm == null) return;

        pm.SetWage(wage);
    }
}
