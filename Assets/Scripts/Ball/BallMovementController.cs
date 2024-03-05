using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BallMovementController : MonoBehaviour
{
    private BallController ballController;
    private Direction swipeSide;
    private Rigidbody rb;
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private bool isSwipe = false;
    private bool canMove = false;
    public bool CanMove { get => canMove; set => canMove = value; }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballController=GetComponent<BallController>();
    }
    private void FixedUpdate()
    {
        if (canMove) { MouseController(); TouchedController(); }
    }

    private void MouseController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerUpPosition = Input.mousePosition;
            fingerDownPosition = Input.mousePosition;
            isSwipe = true;
        }

        if (Input.GetMouseButtonUp(0) && isSwipe)
        {
            isSwipe = false;
            fingerDownPosition = Input.mousePosition;
            ballController.IncreaseCounter();
            SwipeDirectionCheck();
            BallMoveController();
        }
    }
    private void TouchedController()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
                isSwipe = true;
            }

            if (touch.phase == TouchPhase.Ended && isSwipe)
            {
                fingerDownPosition = touch.position;
                ballController.IncreaseCounter();
                SwipeDirectionCheck();
                BallMoveController();
                isSwipe = false;
            }
        }
    }
    private void SwipeDirectionCheck()
    {
        Vector2 distance = fingerDownPosition - fingerUpPosition;
        float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        if (angle < 0)
            angle += 360;

        if (angle >= 45 && angle < 135)
            swipeSide = Direction.Up;
        else if (angle >= 135 && angle < 225)
            swipeSide = Direction.Left;
        else if (angle >= 225 && angle < 315)
            swipeSide = Direction.Down;
        else
            swipeSide = Direction.Right;
    }
    private Vector3 SetVectorOfSwipeDirection()
    {
        if (swipeSide == Direction.Up)
            return Vector3.forward;
        else if (swipeSide == Direction.Down)
            return Vector3.back;
        else if (swipeSide == Direction.Right)
            return Vector3.right;
        else
            return Vector3.left;
    }
    private void BallMoveController()
    {
        if (swipeSide != Direction.None)
        {
            BallMove(SetVectorOfSwipeDirection());
        }
    }
    private void BallMove(Vector3 direction)
    {
        Ray ballRay = new Ray(transform.position, direction);
        RaycastHit ballHit;
        Debug.DrawRay(transform.position, direction, Color.red);
        if (Physics.Raycast(ballRay, out ballHit))
        {
            if (ballHit.collider.GetComponent<Border>())
            {
                Vector3 pos = ballHit.transform.position - direction;
                ballController.PlayAnimation(swipeSide);
                var time = TimeCalculation(pos);
                transform.DOMove(pos, time).SetEase(Ease.Linear).OnComplete(() =>
                {
                    ballController.StopAnimation();
                });
                BallStop();
            }
        }
    }
    private float TimeCalculation(Vector3 pos)
    {
        float difference = Vector3.Distance(transform.position, pos);
        float time = difference / 25;
        return time;
    }
    public void BallStop()
    {
        swipeSide = Direction.None;
        rb.velocity = new Vector3(0, 0, 0);
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
}
