using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "ScriptableObject/Game Mode Data")]
public class GameModeData : ScriptableObject
{
    [SerializeField] private List<Texture2D> levelData;
    [SerializeField] private int levelIndex;
    public Texture2D GetLevelData()
    {
        return levelData[levelIndex];
    }
    public List<Texture2D> GetLevelDataList()
    {
        return levelData;
    }
    public void IncreaseLevel()
    {
        if (levelIndex<levelData.Count-1)
        {
            levelIndex++;
        }
    }
    public void Save()
    {
        SaveManager.SaveGameData(this);
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(GameModeData))]
    public class PlayerDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GameModeData playerData = (GameModeData)target;
            if (GUILayout.Button("Save"))
            {
                playerData.Save();
            }
        }
    }
#endif
}
