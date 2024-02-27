using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallController : MonoBehaviour
{
    private BallMovementController ballMovement;
    private Sequence sequence;
    private Vector3 childFirstScale;

    private void OnEnable()
    {
        EventManager.BallStartPos += StartPos;
    }
    private void OnDisable()
    {
        EventManager.BallStartPos -= StartPos;
    }
    private void Start()
    {
        childFirstScale = transform.GetChild(0).lossyScale;
        sequence = DOTween.Sequence();
        ballMovement = GetComponent<BallMovementController>();
    }
    private void StartPos(Vector3 pos)
    {
        ballMovement.BallStop();
        pos = new Vector3(pos.x, pos.y + 1, pos.z);
        transform.position = pos;
    }
    public void PlayAnimation(Direction direction)
    {
        sequence.Complete();
        Vector3 scale=transform.localScale;
        Vector3 childScale= childFirstScale;
        Vector3 childPos=transform.GetChild(0).localPosition;
        if (direction == Direction.Left || direction == Direction.Right)
        {
            scale.z = 0.5f;
            childScale.x = 3f;
            childPos.x = (direction == Direction.Left) ? 1f : -1f;
        }
        else if (direction == Direction.Down || direction == Direction.Up)
        {
            scale.x = 0.5f;
            childScale.z = 3f;
            childPos.z = (direction == Direction.Down) ? 1f : -1f;
        }
        sequence.Append(transform.DOScale(scale, 0.2f));
        sequence.Join(transform.GetChild(0).DOLocalMove(childPos, 0.2f));
        sequence.Join(transform.GetChild(0).DOScale(childScale, 0.2f));
    }
    public void StopAnimation()
    {
        sequence.Complete();
        sequence.Append(transform.DOScale(Vector3.one, 0.2f));
        sequence.Join(transform.GetChild(0).DOLocalMove(Vector3.zero, 0.2f));
        sequence.Join(transform.GetChild(0).DOScale(childFirstScale, 0.2f));
    }
}
