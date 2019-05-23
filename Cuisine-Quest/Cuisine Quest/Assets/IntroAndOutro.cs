using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class IntroAndOutro : MonoBehaviour 
{
    public GameObject ui;
    public TextMeshProUGUI screenText;
    public Image image;
    private QuestManager questManager;
    private DialogSystemController dialogSystemController;
    [TextArea(1,20)]
    public string intro;
    [TextArea(1,20)]
    public string outro;

    public Animator animator;

    public CanvasGroup canvasGroup;

    private bool finishedGame;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        dialogSystemController = FindObjectOfType<DialogSystemController>();
        if(!File.Exists(Path.Combine(Application.persistentDataPath,"PlayerItems.json")))
        {
            StartIntro();
        }
        else
        {
            canvasGroup.alpha = 0.0f;
            image.enabled = false;
        }
    }

    private void Update()
    {
        if(!finishedGame)
        {
            if (questManager.finishedEveryQuest() && dialogSystemController.isEmpty())
            {
                finishedGame = true;
                image.enabled = true;
                StartOutro();
            }
        }
    }

    public void StartIntro()
    {
        canvasGroup.alpha = 1.0f;
        PauseMenu.paused = true;
        StartCoroutine(printText(true, intro));
    }

    public void StartOutro()
    {
        animator.SetTrigger("End");
        PauseMenu.paused = true;
        StartCoroutine(printText(false, outro));
    }

    IEnumerator printText(bool intro, string text)
    {
        screenText.text = string.Empty;

        foreach (char letter in text)
        {
            screenText.text += letter;
            //Does 1 character on screen every frame
            yield return null;
        }

        if(intro)
        {
            yield return new WaitForSeconds(5.0f);
            animator.SetTrigger("BeginGame");
            screenText.text = string.Empty;
            canvasGroup.alpha = 0.0f;
            PauseMenu.paused = false;
        }
        else
        {
            yield return new WaitForSeconds(5.0f);
            SceneManager.LoadScene("Credits");
        }
    }
}
