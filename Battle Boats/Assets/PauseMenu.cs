using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void BackMenu() {
        Debug.Log("Going back to Welcome Page...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("WelcomePage");

    }

    public void QuitGame() {
        Debug.Log("Quiting Game....");
        Application.Quit();
    }

}
