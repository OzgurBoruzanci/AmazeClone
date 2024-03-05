using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePanelController : MonoBehaviour
{
    private bool isCounter;
    private float timerDuration = 50;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI counterText;
    private Texture2D levelSprites;
    private int levelCounter;
    public bool IsCounter { get=>isCounter; set=>isCounter=value; }
    private void OnEnable()
    {
        EventManager.LevelSprites += LevelSprites;
        EventManager.PuzzleCreated += BeginCounter;
        EventManager.UpdateCounterText += BeginCounter;
    }
    private void OnDisable()
    {
        EventManager.LevelSprites -= LevelSprites;
        EventManager.PuzzleCreated -= BeginCounter;
        EventManager.UpdateCounterText-=BeginCounter;
    }
    private void LevelSprites(Texture2D sprites)
    {
        levelSprites = sprites;
    }
    public void BeginTimer()
    {
        timerDuration = 40;
        StartCoroutine(LevelDurationCoroutine());
    }
    private IEnumerator LevelDurationCoroutine()
    {
        while (timerDuration > 0f)
        {
            int minutes = Mathf.FloorToInt(timerDuration / 60f);
            int seconds = Mathf.FloorToInt(timerDuration % 60f);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return new WaitForSecondsRealtime(1f);

            timerDuration -= 1f;
        }
        GameManager.Instance.GameOver();
        EventManager.FailGame();
    }
    private void BeginCounter()
    {
        if (isCounter)
        {
            CheckCounterLimit();
        }
    }
    private void CheckCounterLimit()
    {
        levelCounter = int.Parse(levelSprites.name);
        int ballCounter = BallController.Instance.Counter;
        int text = levelCounter - ballCounter;
        if (text>=0)
        {
            counterText.text = text.ToString();
        }
        if (ballCounter > levelCounter)
        {
            GameManager.Instance.GameOver();
            EventManager.FailGame();
        }
    }
}
