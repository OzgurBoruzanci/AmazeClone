using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Action<Vector3> BallStartPos;
    public static Func<LevelData> GetLevelDataScriptable;

    public static Func<LevelIndex> GetLevelIndex;

    public static Action/*<PuzzleCube>*/ PuzzleCubeCreated;
    public static Func<GamePreferences> GetGamePreferencesScriptable;

    public static Func<float> GetPuzzlePercentage;
    public static Action CreateLevel;
    public static Action StartGame;

    public static Action/*<List<BorderCube>, int>*/ PuzzleCreated;
    public static Action CheckPuzzlePercentage;
    public static Action PuzzleCubeFilled;
}
