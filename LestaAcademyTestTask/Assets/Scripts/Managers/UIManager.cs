using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public bool IsPaused { get; private set; }

    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameplayCanvas;

    private bool isWInOrDefeat;

    private void Awake()
    {
        Instance = this;
        menuCanvas.SetActive(false);
        gameplayCanvas.SetActive(true);
        IsPaused = false;
        isWInOrDefeat = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isWInOrDefeat)
            {
                IsPaused = !IsPaused;
                menuCanvas.SetActive(IsPaused);
            }
        }
    }

    // ------------------------------------------------- HEALTH BAR ----------------------------------------------- //

    public void CallHealthBarUpdate(float maxHealth, float newHealth)
    {
        gameplayCanvas.TryGetComponent<GameplayCanvas>(out GameplayCanvas temp);
        temp.UpdateHealthBar(maxHealth, newHealth);
    }

    // ------------------------------------------------ FINISH PANEL ---------------------------------------------- //

    public void ShowFinish()
    {
        isWInOrDefeat = true;
        gameplayCanvas.TryGetComponent<GameplayCanvas>(out GameplayCanvas temp);
        temp.ActivateFinishPanel();
        Time.timeScale = 0;
    }

    // ---------------------------------------------- GAME OVER PANEL --------------------------------------------- //

    public void ShowGameover()
    {
        isWInOrDefeat = true;
        gameplayCanvas.TryGetComponent<GameplayCanvas>(out GameplayCanvas temp);
        temp.ActivateGOPanel();
        Time.timeScale = 0;
    }

    // ------------------------------------------------ MENU BUTTONS ---------------------------------------------- //
    public void OnClickResume()
    {
        menuCanvas.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1;
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene(0);
    }

    // --------------------------------------------------- COMMON ------------------------------------------------ //
    public void OnClickRestart()
    {
        Time.timeScale = 1;
        GameManager.Instance.RestartLevel();
    }
}
