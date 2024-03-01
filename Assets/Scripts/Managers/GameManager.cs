using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => s_Instance;
    private static GameManager s_Instance;

    [SerializeField] private new ParticleSystem particleSystem;
    public GameType gameType;
    private void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
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
            particleSystem.Play();
            Invoke("CreateLevelAfterDelay", 2f);
        }
    }
    private void CreateLevelAfterDelay()
    {
        Debug.Log("a");
        particleSystem.Stop();
        EventManager.PuzzleCubeFilled();
        EventManager.CreateLevel();
    }
    public void GameOver()
    {
        EventManager.GameOver();
    }
}
