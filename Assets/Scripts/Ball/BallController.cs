using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    public static BallController Instance => s_Instance;
    private static BallController s_Instance;
    private BallMovementController ballMovement;
    private Sequence sequence;
    public int counter;
    public int Counter
    {
        get => counter;
        set => counter = value;
    }
    
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
        EventManager.BallStartPos += StartGame;
        EventManager.GameOver += GameOver;
    }
    private void OnDisable()
    {
        EventManager.BallStartPos -= StartGame;
        EventManager.GameOver -= GameOver;
    }
    private void Start()
    {
        sequence = DOTween.Sequence();
        ballMovement = GetComponent<BallMovementController>();
    }
    private void StartGame(Vector3 pos)
    {
        transform.GetComponent<TrailRenderer>().enabled = false;
        counter = 0;
        ballMovement.CanMove=true;
        pos = new Vector3(pos.x, pos.y + 0.9f, pos.z);
        transform.position = pos;
        transform.GetComponent<TrailRenderer>().enabled = true;
    }
    public void PlayAnimation(Direction direction)
    {
        sequence.Complete();
        Vector3 scale = transform.localScale;
        if (direction == Direction.Left || direction == Direction.Right)
        {
            scale.z = 0.5f;
        }
        else if (direction == Direction.Down || direction == Direction.Up)
        {
            scale.x = 0.5f;
        }
        sequence.Append(transform.DOScale(scale, 0.2f));
    }
    public void StopAnimation()
    {
        sequence.Complete();
        sequence.Append(transform.DOScale(Vector3.one, 0.2f));
    }
    public void IncreaseCounter()
    {
        counter++;
        EventManager.UpdateCounterText();
    }
    private void GameOver()
    {
        ballMovement.CanMove = false;
        ballMovement.BallStop();
        transform.GetComponent<TrailRenderer>().enabled = false;
        transform.position = new Vector3(-100, 0, 0);
    }
}
