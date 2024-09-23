using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private Image healthImage;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text timerText;

    private void Start()
    {
        finishPanel.SetActive(false);
        gameoverPanel.SetActive(false);
    }

    public void ActivateGOPanel()
    {
        gameoverPanel.SetActive(true);
    }

    public void ActivateFinishPanel()
    {
        float timer = GameManager.Instance.ShareTimer();

        string sum_string;
        string temp_secs;
        string temp_mins;

        int mins = (int)(timer / 60f);
        temp_mins = mins.ToString();

        if (timer >= 60f)
        {
            temp_secs = ((int)(timer - mins * 60)).ToString();
        }
        else
        {
            temp_secs = ((int)timer).ToString();
        }

        sum_string = $"{temp_mins}m {temp_secs}s";

        timerText.text = "Your time: " + sum_string;
        finishPanel.SetActive(true);
    }

    public void UpdateHealthBar(float maxHealth, float newHealth)
    {
        healthText.text = ((int)newHealth).ToString();
        healthImage.fillAmount = (newHealth / maxHealth);
    }
}
