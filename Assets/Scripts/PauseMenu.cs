using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] GameObject PauseMenuUI;
    public static bool paused = false;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            if (paused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        PauseMenuUI.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1f;
        paused = false;
    }
    
    void PauseGame()
    {
        PauseMenuUI.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        paused = true;
    }

    public void BackToMenu() {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenuScene");
    }
    public void QuitApplication() {
        Application.Quit();
    }
}
