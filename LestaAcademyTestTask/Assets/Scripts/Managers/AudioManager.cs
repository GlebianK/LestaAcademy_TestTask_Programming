using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> clips; // 0 - norm, 1 - explicit, 2 - music

    private AudioClip curClip;

    private void Awake()
    {
        if (audioSource == null || clips.Count == 0)
        {
            Debug.LogError($"AudioManager error! audioSource = {audioSource}, clips.Count = {clips.Count}");
            return;
        }

        if (PlayerPrefs.HasKey("Explicit"))
        {
            int temp = PlayerPrefs.GetInt("Explicit");
            if (temp == 0)
            {
                curClip = clips[0];
            }
            else
            {
                curClip = clips[1];
            }
        }
        else
        {
            curClip = clips[0];
        }

        if (PlayerPrefs.HasKey("Music"))
        {
            int temp = PlayerPrefs.GetInt("Music");
            if (temp == 1)
            {
                curClip = clips[2];
            }
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    private void Start()
    {
        if (audioSource == null)
        {

            Debug.LogError($"AudioManager error! audioSource = {audioSource}");
            return;
        }

        audioSource.clip = curClip;
        audioSource.Play();
    }

    private void OnDisable()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
