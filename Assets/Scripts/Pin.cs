using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public float standingThreshold;
    public float distToRaise = 40f;

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start ()
    {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool IsStanding()
    {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        // Mathf.Abs returns absolute value (e.g. -3 becomes the same as 3)
        float tiltInX = Mathf.Abs(270 - rotationInEuler.x);
        float tiltInZ = Mathf.Abs(rotationInEuler.z);

        if (tiltInX < standingThreshold && tiltInZ < standingThreshold)
        {
            return true;
        }

        return false;
    }

    public void RaiseIfStanding()
    {
        if (IsStanding())
        {
            rigidbody.useGravity = false;
            transform.Translate(new Vector3(0f, distToRaise, 0f), Space.World);
            this.transform.rotation = Quaternion.Euler(270f, 0, 0);
        }
    }

    public void Lower()
    {
        transform.Translate(new Vector3(0f, -distToRaise, 0f), Space.World);
        rigidbody.useGravity = true;
    }
}
