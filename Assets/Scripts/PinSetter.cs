using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public GameObject pinSet;

    private Animator animator;
    private PinCounter pinCounter;

    ActionMasterOld actionMaster;

    // Use this for initialization
    void Start ()
    {
        actionMaster = new ActionMasterOld();
        animator = GetComponent<Animator>();
        pinCounter = GetComponent<PinCounter>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void RaisePins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.RaiseIfStanding(); 
        }
    }

    public void LowerPins()
    {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    public void RenewPins()
    {
        GameObject newPins = Instantiate(pinSet);
        newPins.transform.position += new Vector3(0, 20, 0);
    }

    public void PerformAction(ActionMasterOld.Action action)
    {
        if (action == ActionMasterOld.Action.Tidy)
        {
            animator.SetTrigger("tidyTrigger");
        }
        else if (action == ActionMasterOld.Action.EndTurn)
        {
            animator.SetTrigger("resetTrigger");
            pinCounter.Reset();
        }
        else if (action == ActionMasterOld.Action.Reset)
        {
            animator.SetTrigger("resetTrigger");
            pinCounter.Reset();
        }
        else if (action == ActionMasterOld.Action.EndGame)
        {
            throw new UnityException("Don't know how to handle end game yet");
        }
    }

    //public void Reset()
    //{
    //    animator.SetTrigger("resetTrigger");
    //}

    //public void Tidy()
    //{
    //    animator.SetTrigger("tidyTrigger");
    //}  
}
