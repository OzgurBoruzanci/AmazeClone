using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameType gameType;
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
        EventManager.StartGame();
    }
    public void ChooseGameType()
    {
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
}
