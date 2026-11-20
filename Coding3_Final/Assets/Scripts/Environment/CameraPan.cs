using System;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    //TODO: make background move left and right in time with mouse movement with a clamp
    //TODO: make triggers for left and right movement disappear when clamp value is reached
    
    //REFERENCES:
    //transform holding background and all its elements
    //bounds for the background
    //bools for moving left and right
    //ints for move speed
    //int for framerate so camera moves smoothly
    //call to LeftTrigger and RightTrigger for movement events
    public Transform backgroundHolder;
    public Bounds bgBounds;
    public bool movingLeft, movingRight;
    public int moveSpeed;
    public int targetFrameRate;
    private LeftTrigger leftTrigger;
    private RightTrigger rightTrigger;
    private void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        bgBounds = backgroundHolder.GetComponent<BoxCollider2D>().bounds;
        leftTrigger = GameObject.FindAnyObjectByType<LeftTrigger>();
        rightTrigger = GameObject.FindAnyObjectByType<RightTrigger>();
        leftTrigger.leftTriggered.AddListener(CheckLeft);
        rightTrigger.rightTriggered.AddListener(CheckRight);
    }

    private void Update()
    {
        CameraMovement();
    }

    public void CameraMovement()
    {
        if (movingRight)
        {
            if (transform.position.x >= bgBounds.max.x)
            {
                return;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0, moveSpeed, 0), Time.deltaTime * 2f);
            }
        }
        else if (movingLeft)
        {
            if (transform.position.x <= bgBounds.min.x)
            {
                return;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(0, moveSpeed, 0), Time.deltaTime * 2f);
            }
        }
    }

    public void CheckLeft(bool left)
    {
        movingLeft = left;
    }

    public void CheckRight(bool right)
    {
        movingRight = right;
    }
}
