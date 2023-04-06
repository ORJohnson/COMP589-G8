using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InstructionsMenu : MonoBehaviour
{
    public void BackButton() {
        Debug.Log("Going back to Welcome Page...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("WelcomePage");

    }
}
