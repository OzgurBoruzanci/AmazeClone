using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject timerText;
    [SerializeField] private GameObject counterText;
    [SerializeField] private GameObject failPanel;

    [SerializeField] private Button normalButton;
    [SerializeField] private Button timerButton;
    [SerializeField] private Button counterButton;
    private GamePanelController gamePanelController;

    private void OnEnable()
    {
        EventManager.FailGame += FailGame;
    }
    private void OnDisable()
    {
        EventManager.FailGame -= FailGame;
    }
    private void Start()
    {
        gamePanelController = GetComponent<GamePanelController>();
        ShowSelectedMode(EventManager.GetGameModeScriptable().GetGameMode());
    }
    public void PlayGameBtn()
    {
        Time.timeScale = 1;
        MenuPanel.SetActive(false);
        ActivateGamePanel(EventManager.GetGameModeScriptable().GetGameMode());
        GameManager.Instance.PlayGame();
    }
    public void PauseGameBtn()
    {
        Time.timeScale = 0;
        GamePanel.SetActive(false);
        PausePanel.SetActive(true);
    }
    public void CointuneGameBtn()
    {
        Time.timeScale = 1;
        GamePanel.SetActive(true);
        PausePanel.SetActive(false);
    }
    private void FailGame()
    {
        failPanel.SetActive(true);
        GamePanel.SetActive(false);
    }
    public void TryBtn()
    {
        Time.timeScale = 1;
        failPanel.SetActive(false);
        ActivateGamePanel(EventManager.GetGameModeScriptable().GetGameMode());
        GameManager.Instance.PlayGame();
    }
    public void MainMenuBtn()
    {
        failPanel.SetActive(false);
        MenuPanel.SetActive(true);
        PausePanel.SetActive(false);
        GamePanel.SetActive(false);
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
                gamePanelController.IsCounter = false;
                counterText.SetActive(false);
                break;
            case GameType.Timer:
                timerText.SetActive(true);
                gamePanelController.IsCounter = false;
                gamePanelController.BeginTimer();
                counterText.SetActive(false);
                break;
            case GameType.Counter:
                counterText.SetActive(true);
                gamePanelController.IsCounter = true;
                timerText.SetActive(false);
                break;
        }
    }
}
