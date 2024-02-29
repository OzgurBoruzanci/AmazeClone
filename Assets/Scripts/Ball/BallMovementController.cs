using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementController : MonoBehaviour
{
    private BallController ballController;
    private Direction swipeSide;
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;
    private Rigidbody rb;
    private bool isSwipe = false;
    private bool canMove = true; //false
    public bool CanMove 
    {
        get => canMove;
        set => canMove = value; 
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballController=GetComponent<BallController>();
    }
    private void FixedUpdate()
    {
        BallMoveController();
    }
    private void Update()
    {
        if (canMove) { MouseController(); TouchedController(); }
    }

    private void MouseController()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fingerUpPosition = Input.mousePosition;
            fingerDownPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            fingerDownPosition = Input.mousePosition;
            SwipeDirectionCheck();
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
                SwipeDirectionCheck();
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
            BallMove(SetVectorOfSwipeDirection(), Time.deltaTime);
            EventManager.CheckPuzzlePercentage();
        }
    }
    private void BallMove(Vector3 direction, float time)
    {
        Ray ballRay = new Ray(transform.position, direction/4);
        RaycastHit ballHit;
        Debug.DrawRay(transform.position, direction, Color.red);
        if (Physics.Raycast(ballRay, out ballHit, 1))
        {
            if (ballHit.collider != null)
            {
                BallStop();
                ballController.Counter++;
            }
        }
        else
        {
            transform.Translate(direction * time * 50);
            ballController.PlayAnimation(swipeSide);
            
            //transform.localScale = new Vector3(1, 1, 0.7f);
        }
    }

    public void BallStop()
    {
        ballController.StopAnimation();
        swipeSide = Direction.None;
        rb.velocity = new Vector3(0, 0, 0);
        this.transform.Translate(new Vector3(0, 0, 0));
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }
}
