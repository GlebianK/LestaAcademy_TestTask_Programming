using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MMManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorPanel;
    [SerializeField] private Toggle explicitToggle;
    [SerializeField] private Toggle musicToggle;

    private void Start()
    {
        tutorPanel.SetActive(false);

        if (PlayerPrefs.HasKey("Music"))
        {
            switch (PlayerPrefs.GetInt("Music"))
            {
                case 0:
                    musicToggle.isOn = false;
                    break;
                case 1:
                    musicToggle.isOn = true;
                    break;
                default:
                    Debug.LogError("Wrong value for MUSIC key!!!");
                    break;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            musicToggle.isOn = false;
        }

        if (PlayerPrefs.HasKey("Explicit"))
        {
            switch(PlayerPrefs.GetInt("Explicit"))
            {
                case 0: 
                    explicitToggle.isOn = false;
                    break;
                case 1:
                    explicitToggle.isOn = true;
                    break;
                default:
                    Debug.LogError("Wrong value for EXPLICIT key!!!");
                    break;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Explicit", 0);
            explicitToggle.isOn = false;
        }
    }

    // ======================================= CHECKMARKS ========================= //

    public void OnChangeMusicState()
    {
        if (musicToggle.isOn)
        {
            PlayerPrefs.SetInt("Music", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    public void OnChangeExplicitState()
    {
        if(explicitToggle.isOn)
        {
            PlayerPrefs.SetInt("Explicit", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Explicit", 0);
        }    
    }

    // ========================================= BUTTONS =========================== //

    public void OnClickPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnClickControls()
    {
        tutorPanel.SetActive(true);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
