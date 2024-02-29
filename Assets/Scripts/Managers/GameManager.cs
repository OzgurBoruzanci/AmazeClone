using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => s_Instance;
    private static GameManager s_Instance;

    [SerializeField] public GameType gameType;
    public UnityEvent Start_Game;
    public UnityEvent Game_Over;
    private void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
        Start_Game.AddListener(PlayGame);
        Game_Over.AddListener(GameOver);
    }
    private void OnEnable()
    {
        ChooseGameType();
        EventManager.CheckPuzzlePercentage += CheckPuzzlePercentage;
    }
    private void OnDisable()
    {
        EventManager.CheckPuzzlePercentage -= CheckPuzzlePercentage;
    }
    public void PlayGame()
    {
        ChooseGameType();
        EventManager.CreateLevel();
        Start_Game.Invoke();
    }
    public void ChooseGameType()
    {
        gameType = EventManager.GetGameModeScriptable().GetGameMode();
        EventManager.GetLevelIndex().GameType = gameType;
        EventManager.GetLevelDataScriptable().GameType = gameType;
    }
    private void CheckPuzzlePercentage()
    {
        float percentage = EventManager.GetPuzzlePercentage();
        if (percentage>=100)
        {
            EventManager.PuzzleCubeFilled();
            EventManager.CreateLevel();
        }
    }
    public void GameOver()
    {
        Game_Over.Invoke();
    }
}
