using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Level Data")]
public class LevelData : ScriptableObject
{
    public GameType GameType;
    [SerializeField] private List<Texture2D> levelData;
    [SerializeField] private List<Texture2D> counterLevelData;
    [SerializeField] private List<Texture2D> timerLevelData;
    public List<Texture2D> GetLevelData()
    {
        switch (GameType)
        {
            case GameType.Normal:
                return levelData;
            case GameType.Timer:
                return timerLevelData;
            case GameType.Counter:
                return counterLevelData;
            default: return levelData;
        }
    }
}
