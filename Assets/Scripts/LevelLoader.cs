using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private LevelIndex levelIndex;
    private void OnEnable()
    {
        EventManager.PuzzleCubeFilled += PuzzleCubeFilled;
    }
    private void OnDisable()
    {
        EventManager.PuzzleCubeFilled -= PuzzleCubeFilled;
    }
    private void Start()
    {
        LoadLevel();
    }
    public void LoadLevel()
    {
        SaveManager.LoadGameData(levelIndex);

        //SceneManager.LoadScene(1);
    }
    private void PuzzleCubeFilled()
    {
        levelIndex.IncreaseLevel();
        SaveManager.SaveGameData(levelIndex);
        LoadLevel();
    }
}
