using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "ScriptableObject/Level Index")]
public class LevelIndex : ScriptableObject
{
    public GameType GameType;
    //public int levelIndex;
    [SerializeField] private int timeIndex;
    [SerializeField] private int counterIndex;
    [SerializeField] private int normalIndex;

    public int GetLevelIndex()
    {
        switch (GameType)
        {
            case GameType.Normal:
                return normalIndex;
            case GameType.Timer:
                return timeIndex;
            case GameType.Counter:
                return counterIndex;
            default: return normalIndex;
        }
    }
    public void IncreaseLevel()
    {
        switch (GameType)
        {
            case GameType.Normal:
                 normalIndex++;
                break;
            case GameType.Timer:
                 timeIndex++;
                break;
            case GameType.Counter:
                 counterIndex++;
                break;
        }
    }
    public void Save()
    {
        SaveManager.SaveGameData(this);
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(LevelIndex))]
    public class PlayerDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            LevelIndex playerData = (LevelIndex)target;
            if (GUILayout.Button("Save"))
            {
                playerData.Save();
            }
        }
    }
#endif
}
