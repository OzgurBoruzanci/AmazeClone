using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private GamePreferences gamePreferences;
    [SerializeField] private LevelIndex levelIndex;
    void OnEnable()
    {
        EventManager.GetLevelDataScriptable += GetLevelDataScriptable;
        EventManager.GetGamePreferencesScriptable += GetGamePreferencesScriptable;
        EventManager.GetLevelIndex += GetLevelIndex;
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

    }

    private LevelData GetLevelDataScriptable()
    {
        return levelData;
    }
}
