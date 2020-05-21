using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Ball))]
public class BallDragLaunch : MonoBehaviour
{
    private Vector3 dragStart, dragEnd;
    private float startTime, endTime;
    private Ball ball;

	// Use this for initialization
	void Start ()
    {
        ball = GetComponent<Ball>();
	}

    // Capture time and position of drag start
    public void DragStart()
    {
        if (!ball.inPlay)
        {
            dragStart = Input.mousePosition;
            startTime = Time.time;
        }
    }

    // Launch the ball
    public void DragEnd()
    {
        if(!ball.inPlay)
        {
            ball.inPlay = true;

            dragEnd = Input.mousePosition;
            endTime = Time.time;

            float launchSpeedX = (dragEnd.x - dragStart.x) / (endTime - startTime);
            float launchSpeedZ = (dragEnd.y - dragStart.y) / (endTime - startTime);

            Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);

            ball.Launch(launchVelocity);
        }        
    }

    public void MoveStart(float xNudge)
    {
        // Clamp ball within lane width
        bool withinLane = true;
        if ((ball.transform.position.x + xNudge) < -50f || (ball.transform.position.x + xNudge) > 50f)
        {
            withinLane = false;
        }

        // Move ball if allowed
        if (!ball.inPlay && withinLane)
        {
            ball.transform.Translate(new Vector3(xNudge, 0, 0));
        }

        //Debug.Log("Ball moved" + xNudge);

    }
}
