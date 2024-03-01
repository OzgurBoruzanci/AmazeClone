using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Func<LevelData> GetLevelDataScriptable;
    public static Func<LevelIndex> GetLevelIndex;
    public static Func<GamePreferences> GetGamePreferencesScriptable;
    public static Func<GameMode> GetGameModeScriptable;

    public static Action<GameType> SetGameType;
    public static Action<Texture2D> LevelSprites;
    public static Action<Vector3> BallStartPos;
    public static Action CreateLevel;

    public static Func<float> GetPuzzlePercentage;
    public static Action CheckPuzzlePercentage;
    public static Action PuzzleCubeFilled;
    public static Action GameOver;
}
