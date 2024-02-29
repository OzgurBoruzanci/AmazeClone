using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Game Mode")]
public class GameMode : ScriptableObject
{
    [SerializeField] private GameType gameType;

    public void SaveType(GameType type)
    {
        gameType = type;
        SaveManager.SaveGameData(this);
    }
    public GameType GetGameMode()
    {
        SaveManager.LoadGameData(this);
        return gameType;
    }
}
