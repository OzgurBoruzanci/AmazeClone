using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject TimerPanel;
    [SerializeField] private GameObject CounterPanel;
    [SerializeField] private GameObject timerText;
    [SerializeField] private GameObject counterText;

    [SerializeField] private Button normalButton;
    [SerializeField] private Button timerButton;
    [SerializeField] private Button counterButton;
    private void Start()
    {
        ShowSelectedMode(EventManager.GetGameModeScriptable().GetGameMode());
    }
    public void PlayGameBtn()
    {
        MenuPanel.SetActive(false);
        ActivateGamePanel(EventManager.GetGameModeScriptable().GetGameMode());
        GameManager.Instance.PlayGame();
    }
    public void PauseGameBtn()
    {
        GamePanel.SetActive(false);
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void CointuneGameBtn()
    {
        GamePanel.SetActive(true);
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void MainMenuBtn()
    {
        MenuPanel.SetActive(true);
        PausePanel.SetActive(false);
        GameManager.Instance.GameOver();
    }
    public void NormalBtn()
    {
        EventManager.GetGameModeScriptable().SaveType(GameType.Normal);
        ShowSelectedMode(GameType.Normal);
    }
    public void TimerBtn()
    {
        EventManager.GetGameModeScriptable().SaveType(GameType.Timer);
        ShowSelectedMode(GameType.Timer);
    }
    public void CounterBtn()
    {
        EventManager.GetGameModeScriptable().SaveType(GameType.Counter);
        ShowSelectedMode(GameType.Counter);
    }
    private void ShowSelectedMode(GameType mode)
    {
        normalButton.interactable = true;
        timerButton.interactable = true;
        counterButton.interactable = true;

        switch (mode)
        {
            case GameType.Normal:
                normalButton.interactable = false;
                break;
            case GameType.Timer:
                timerButton.interactable = false;
                break;
            case GameType.Counter:
                counterButton.interactable = false;
                break;
        }
    }
    private void ActivateGamePanel(GameType mode)
    {
        GamePanel.SetActive(true);
        switch (mode)
        {
            case GameType.Normal:
                timerText.SetActive(false);
                counterText.SetActive(false);
                break;
            case GameType.Timer:
                timerText.SetActive(true);
                counterText.SetActive(false);
                break;
            case GameType.Counter:
                counterText.SetActive(true);
                timerText.SetActive(false);
                break;
        }
    }
}
