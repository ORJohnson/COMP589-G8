using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartBattle()
    {
        // SceneManager.LoadScene("Ships");
         SceneManager.LoadScene("BoardScene");
    }

    public void ExitGame() {
        Debug.Log("Exiting.....");
        Application.Quit();
        SceneManager.LoadScene("ExitPage");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("InstructionsPage");
    }

}
