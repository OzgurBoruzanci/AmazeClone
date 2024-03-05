using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    private GameModeData modeData;
    private GameMode gameMode;
    private GameType gameType;
    private int timerListSize;
    private int levelGained;
    private void OnEnable()
    {
        EventManager.PuzzleCubeFilled += PuzzleCubeFilled;
        EventManager.PlayGame += LoadLevel;
    }
    private void OnDisable()
    {
        EventManager.PuzzleCubeFilled -= PuzzleCubeFilled;
        EventManager.PlayGame-= LoadLevel;
    }
    public void LoadLevel()
    {
        modeData=EventManager.GetGameModeDataScriptable();
        gameMode = EventManager.GetGameModeScriptable();
        gameType = gameMode.GetGameMode();
        timerListSize = modeData.GetLevelDataList().Count-1;
        SaveManager.LoadGameData(modeData);
    }
    private void PuzzleCubeFilled()
    {
        var levelSize = timerListSize - levelGained;
        Debug.Log(levelSize);
        if (gameType==GameType.Timer)
        {
            if (levelSize > 0)
            {
                levelGained++;
                GameManager.Instance.Delay = 0.1f;
            }
            else if (levelSize <= 0)
            {
                EventManager.LevelSuccessful();
                GameManager.Instance.Delay = 2f;
                levelGained = 0;
            }
        }
        else
        {
            EventManager.LevelSuccessful();
            GameManager.Instance.Delay = 2f;
        }
        modeData.IncreaseLevel();
        SaveManager.SaveGameData(modeData);
        LoadLevel();
    }
}
