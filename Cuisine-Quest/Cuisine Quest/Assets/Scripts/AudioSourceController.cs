using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioSourceController : MonoBehaviour
{
    private static AudioSourceController instance = null;

    private static Dictionary<string, AudioClip> audioClipSources;

    public static AudioSourceController Instance
    {
        get { return instance; }
    }

    private AudioSource audioSource;
    public List<AudioClip> audioClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();

        audioClipSources = new Dictionary<string, AudioClip>();

        foreach (AudioClip clip in audioClips)
        {
            audioClipSources.Add(clip.name, clip);
        }
    }

    public void PlayAudio(string name)
    {
        audioSource.clip = audioClipSources[name];
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayAudioLooped(string name)
    {
        audioSource.clip = audioClipSources[name];
        audioSource.loop = true;
        audioSource.Play();
    }

    public float GetLengthOfSong(string name)
    {
        audioSource.clip = audioClipSources[name];
        return audioSource.clip.length;
    }

    public float GetLengthOfCurrentSong()
    {
        if(audioClips.Count > 0)
        {
            return audioSource.clip.length;
        }
        return 0;
    }

    public IEnumerator PlayFieldMusic()
    {
        PlayAudio("FieldIntro");
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        PlayAudioLooped("FieldMusic");
    }

    public IEnumerator PlaySaveMusic()
    {
        PlayAudio("Save");
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        PlayAudioLooped("FieldMusic");
    }
}