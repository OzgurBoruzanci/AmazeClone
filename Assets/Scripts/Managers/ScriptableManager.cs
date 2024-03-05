using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableManager : MonoBehaviour
{
    [SerializeField] private GamePreferences gamePreferences;
    [SerializeField] private GameModeData normalMode;
    [SerializeField] private GameModeData timerMode;
    [SerializeField] private GameModeData counterMode;
    [SerializeField] private GameMode gameMode;
    void OnEnable()
    {
        EventManager.GetGamePreferencesScriptable += GetGamePreferencesScriptable;
        EventManager.GetGameModeScriptable += GetGameMode;
        EventManager.GetGameModeDataScriptable += GetGameModeData;
    }
    private GameModeData GetGameModeData()
    {
        switch(gameMode.GetGameMode())
        {
            case GameType.Normal:
                return normalMode;
            case GameType.Timer:
                return timerMode;
            case GameType.Counter:
                return counterMode;
            default:
                return normalMode;
        }
    }

    private GamePreferences GetGamePreferencesScriptable()
    {
        return gamePreferences;
    }

    void OnDisable()
    {
        EventManager.GetGamePreferencesScriptable -= GetGamePreferencesScriptable;
        EventManager.GetGameModeScriptable -= GetGameMode;
        EventManager.GetGameModeDataScriptable -= GetGameModeData;
    }
    private GameMode GetGameMode()
    {
        return gameMode;
    }
}
