using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseManager : MonoBehaviour
{
    //listen for win or lose event from GameManager
    //change scene
    public void OnGameWin(int winIndex)
    {
        SceneManager.LoadScene(winIndex);
    }
    
    public void OnGameLose(int loseIndex)
    {
        SceneManager.LoadScene(loseIndex);
    }
}
