using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private GamePreferences gamePreferences;
    [SerializeField] private LevelIndex levelIndex;
    [SerializeField] private GameMode gameMode;
    void OnEnable()
    {
        EventManager.GetLevelDataScriptable += GetLevelDataScriptable;
        EventManager.GetGamePreferencesScriptable += GetGamePreferencesScriptable;
        EventManager.GetLevelIndex += GetLevelIndex;
        EventManager.GetGameModeScriptable += GetGameMode;
    }

    private LevelIndex GetLevelIndex()
    {
        return levelIndex;
    }

    private GamePreferences GetGamePreferencesScriptable()
    {
        return gamePreferences;
    }

    void OnDisable()
    {
        EventManager.GetLevelDataScriptable -= GetLevelDataScriptable;
        EventManager.GetGamePreferencesScriptable -= GetGamePreferencesScriptable;
        EventManager.GetLevelIndex -= GetLevelIndex;
        EventManager.GetGameModeScriptable -= GetGameMode;
    }
    private GameMode GetGameMode()
    {
        return gameMode;
    }
    private LevelData GetLevelDataScriptable()
    {
        return levelData;
    }
}
