using UnityEngine.Events;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    //FINISHED: make background move left and right in time with mouse movement with a clamp
    //TODO: make triggers for left and right movement disappear when clamp value is reached
    
    //REFERENCES:
    //transform holding background and all its elements
    //bounds for the background
    //bools for moving left and right
    //ints for move speed
    //int for framerate so camera moves smoothly
    //call to LeftTrigger and RightTrigger for movement events
    //events for when camera reaches max left and right clamp values
    public Transform backgroundHolder;
    public Bounds bgBounds;
    
    public bool movingLeft, movingRight;
    public int moveSpeed = 20;
    public int targetFrameRate = 60;
    
    private LeftTrigger leftTrigger;
    private RightTrigger rightTrigger;

    public UnityEvent<bool> maxLeft;
    public UnityEvent<bool> maxRight;
    
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
                maxRight.Invoke(true);
                movingRight = false;
            }
            else
            {
                maxRight.Invoke(false);
                transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(moveSpeed, 0, 0), Time.deltaTime * 2f);
            }
        }
        else if (movingLeft)
        {
            if (transform.position.x <= bgBounds.min.x)
            {
                maxLeft.Invoke(true);
                movingLeft = false;
            }
            else
            {
                maxLeft.Invoke(false);
                transform.position = Vector3.Lerp(transform.position, transform.position - new Vector3(moveSpeed, 0, 0), Time.deltaTime * 2f);
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
