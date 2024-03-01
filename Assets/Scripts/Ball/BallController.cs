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
    private Vector3 childFirstScale;
    private int counter = 0;
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
        EventManager.BallStartPos += StartPos;
        EventManager.GameOver += GameOver;
    }
    private void OnDisable()
    {
        EventManager.BallStartPos -= StartPos;
        EventManager.GameOver -= GameOver;
    }
    private void Start()
    {
        childFirstScale = transform.GetChild(0).lossyScale;
        sequence = DOTween.Sequence();
        ballMovement = GetComponent<BallMovementController>();
    }
    private void StartPos(Vector3 pos)
    {
        ballMovement.CanMove=true;
        ballMovement.BallStop();
        pos = new Vector3(pos.x, pos.y + 1, pos.z);
        transform.position = pos;
    }
    private void GameOver()
    {
        ballMovement.CanMove = false;
        ballMovement.BallStop();
    }
}
