using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    private float timerDuration = 50;
    private TextMeshProUGUI timerText;
    void Start()
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
}
