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
    }
    private void OnDisable()
    {
        EventManager.LevelSprites -= LevelSprites;
    }
    private void LevelSprites(Texture2D sprites)
    {
        levelSprites = sprites;
    }

    void Update()
    {
        BeginCounter();
    }
    public void BeginTimer()
    {
        StartCoroutine(LevelDurationCoroutine());
    }
    private IEnumerator LevelDurationCoroutine()
    {
        while (timerDuration > 0f)
        {
            int minutes = Mathf.FloorToInt(timerDuration / 60f);
            int seconds = Mathf.FloorToInt(timerDuration % 60f);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(1f);

            timerDuration -= 1f;
        }
        GameManager.Instance.GameOver();
    }
    private void BeginCounter()
    {
        if (isCounter)
        {
            levelCounter = int.Parse(levelSprites.name); //düzenlenmesi lazým ****
            if (levelCounter != 0)
            {
                CheckCounterLimit();
            }
        }
    }
    private void CheckCounterLimit()
    {
        levelCounter = int.Parse(levelSprites.name);
        int ballCounter = BallController.Instance.Counter;
        int text = levelCounter - ballCounter;
        counterText.text = text.ToString();
        if (ballCounter > levelCounter)
        {
            GameManager.Instance.GameOver();
        }
    }
}
