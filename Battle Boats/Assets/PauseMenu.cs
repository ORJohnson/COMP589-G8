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
        AudioListener.pause = isGamePaused;
    }

    void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        AudioListener.pause = isGamePaused;
    }

    void BackMenu() {
        Debug.Log("Going back to Welcome Page...");
        Time.timeScale = 1f;
        SceneManager.LoadScene("WelcomePage");

    }

    void QuitGame() {
        Debug.Log("Quiting Game....");
        Application.Quit();
        Time.timeScale = 1f;
        SceneManager.LoadScene("ExitPage");
    }

}
