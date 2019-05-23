using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour 
{
    public GameObject pauseMenuUI;
    public static bool paused;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if(paused)
            {
                PauseTime();
            }
            else
            {
                ResumeTime();
            }
        }
    }

    public void PauseTime()
    {
        paused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeTime()
    {
        paused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        ResumeTime();
        AudioSourceController.Instance.PlayAudioLooped("MainMenu");
        SceneManager.LoadScene("MainMenu");
    }
}
