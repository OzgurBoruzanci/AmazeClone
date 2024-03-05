using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => s_Instance;
    private static GameManager s_Instance;

    [SerializeField] private new ParticleSystem particleSystem;
    public GameType gameType;
    private float delay;
    public float Delay { get=>delay; set=>delay=value; }
    private void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        s_Instance = this;
    }
    private void OnEnable()
    {
        EventManager.CheckPuzzlePercentage += CheckPuzzlePercentage;
        EventManager.LevelSuccessful += LevelSuccessful;
    }
    private void OnDisable()
    {
        EventManager.CheckPuzzlePercentage -= CheckPuzzlePercentage;
        EventManager.LevelSuccessful -= LevelSuccessful;
    }
    public void PlayGame()
    {
        EventManager.PlayGame();
    }
    private void CheckPuzzlePercentage()
    {
        float percentage = EventManager.GetPuzzlePercentage();
        if (percentage>=100)
        {
            EventManager.PuzzleCubeFilled();
            BallController.Instance.gameObject.GetComponent<TrailRenderer>().enabled = false;
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(delay);
            sequence.AppendCallback(LevelCompleted);
        }
    }
    private void LevelSuccessful()
    {
        particleSystem.Play();
    }
    private void LevelCompleted()
    {
        EventManager.CreateLevel();
    }
    public void GameOver()
    {
        EventManager.GameOver();
    }
}
