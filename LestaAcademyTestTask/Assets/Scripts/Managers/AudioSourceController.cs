using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    [SerializeField] private List<string> actions;
    [SerializeField] private List<AudioClip> clips_norm;
    [SerializeField] private List<AudioClip> clips_explicit;
    [SerializeField] private AudioSource audioSource;
    [SerializeField, Range(0, 100)] private int targetVolume;

    private List<AudioClip> clips;
    private Dictionary<string, AudioClip> dict;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Explicit"))
        {
            int temp = PlayerPrefs.GetInt("Explicit");
            if (temp == 0)
            {
                clips = clips_norm;
            }
            else
            {
                clips = clips_explicit;
            }
        }
        else
        {
            clips = clips_norm;
        }

        audioSource.volume = ((float)targetVolume)/100f;
        dict = new Dictionary<string, AudioClip>();
        if (actions.Count == clips.Count && actions.Count != 0)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                dict.Add(actions[i], clips[i]);
            }
        }
        else
        {
            Debug.LogError($"AUDIO LISTS HAVE DIFFERENT LENGTH !!! Actions: {actions.Count}, Clips = {clips.Count}");
        }
    }

    public void PlayClip(string actionName)
    {
        dict.TryGetValue(actionName, out AudioClip temp);
        if (temp != null)
        {
            audioSource.clip = temp;
            audioSource.Play();
        }
    }
}
