using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartBattle()
    {
         SceneManager.LoadScene("BoardScene");
    }

    public void ExitGame() {
        Debug.Log("Exiting.....");
        SceneManager.LoadScene("ExitPage");
        Application.Quit();
    }

    public void Instructions()
    {
        SceneManager.LoadScene("InstructionsPage");
    }

}
