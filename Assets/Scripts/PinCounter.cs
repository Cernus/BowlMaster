using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour
{
    public Text standingDisplay;

    private GameManager gameManager;
    private bool ballOutOfPlay;
    private int lastStandingCount;
    private int lastSettledCount;
    private float lastChangeTime;

    // Use this for initialization
    void Start ()
    {
        lastSettledCount = 10;
        ballOutOfPlay = false;
        lastStandingCount = -1;

        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        standingDisplay.text = CountStanding().ToString();

        if (ballOutOfPlay)
        {
            UpdateStandingCountAndSettle();
            standingDisplay.color = Color.red;
        }
    }

    public void Reset()
    {
        lastSettledCount = 10;
    }

    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Ball")
        {
            ballOutOfPlay = true;
        }
    }

    // Update lastStandingCount
    // Call PinsHaveSettled() when they have
    private void UpdateStandingCountAndSettle()
    {
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f; // How long to wait to consider pins settled
        if ((Time.time - lastChangeTime) > settleTime) // If last change > 3s ago
        {
            PinsHaveSettled();
        }
    }

    private void PinsHaveSettled()
    {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        gameManager.Bowl(pinFall);

        lastStandingCount = -1; // Indicates pins have settled and ball not back in box
        ballOutOfPlay = false;
        standingDisplay.color = Color.green;
    }

    public int CountStanding()
    {
        int standingPins = 0;

        // For each Pin in the scene
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                standingPins++;
            }
        }

        return standingPins;
    }
}
