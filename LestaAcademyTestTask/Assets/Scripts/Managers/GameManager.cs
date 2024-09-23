using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    [SerializeField] private float timerPrecision = 0.25f;
    [SerializeField] private float timer;

    private bool isFinished;

    private void Awake()
    {
        Instance = this;
        timer = 0f;
        isFinished = false;

        if (!PlayerPrefs.HasKey("Explicit"))
            PlayerPrefs.SetInt("Explicit", 0);
    }

    public void StartTimer()
    {
        StartCoroutine(LevelTimer());
    }

    public void RestartLevel()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Lose()
    {
        UIManager.Instance.ShowGameover();
        Time.timeScale = 0;
    }

    public void FinishLevel()
    {
        isFinished = true;
        UIManager.Instance.ShowFinish();
    }

    public float ShareTimer()
    {
        return timer;
    }

    private IEnumerator LevelTimer()
    {
        while (!isFinished)
        {
            yield return new WaitForSeconds(timerPrecision);
            timer += timerPrecision;
            yield return null;
        }
        
        yield return null;
    }
}
