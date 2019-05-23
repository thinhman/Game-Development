using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour 
{
    public Image gameOverPanel;
    public Image selectionPanel;

    private void Start()
    {
        Invoke("AllowSelection", 3.0f);
        AudioSourceController.Instance.PlayAudioLooped("Death");
    }

    public void AllowSelection()
    {
        gameOverPanel.gameObject.SetActive(false);
        selectionPanel.gameObject.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("TestingSaveUIQuests");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
