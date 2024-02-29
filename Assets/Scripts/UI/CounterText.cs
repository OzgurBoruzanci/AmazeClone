using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterText : MonoBehaviour
{
    private TextMeshProUGUI counterText;
    private Texture2D levelSprites;
    private int levelCounter;

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
    void Start()
    {
        counterText=GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        levelCounter = int.Parse(levelSprites.name);
        Debug.Log(levelCounter); //düzenlenmesi lazým ****
        if (levelCounter!=0)
        {
            CheckCounterLimit();
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
