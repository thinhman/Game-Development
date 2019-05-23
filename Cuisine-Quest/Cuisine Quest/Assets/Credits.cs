using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour 
{
    public GameObject credits;
    public int speed;

    private void Start()
    {
        StartCoroutine(PlayCredits());
    }

    private void Update()
    {
        credits.transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    IEnumerator PlayCredits()
    {
        AudioSourceController.Instance.PlayAudio("CreditIntro");
        yield return new WaitForSeconds(AudioSourceController.Instance.GetLengthOfCurrentSong());
        AudioSourceController.Instance.PlayAudioLooped("CreditMusic");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
