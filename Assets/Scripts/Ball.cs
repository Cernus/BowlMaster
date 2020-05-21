using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 launchVelocity;
    public bool inPlay;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private Vector3 ballStartPos;

	// Use this for initialization
	void Start ()
    {
        ballStartPos = this.transform.position;
        inPlay = false;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        //Launch(launchVelocity);
    }

    public void Launch(Vector3 v)
    {
        rigidBody.useGravity = true;
        rigidBody.velocity = v;

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void Reset()
    {
        inPlay = false;
        this.transform.position = ballStartPos;
        this.transform.rotation = Quaternion.identity;
        this.rigidBody.velocity = Vector3.zero;
        this.rigidBody.angularVelocity = Vector3.zero;
        this.rigidBody.useGravity = false;
        audioSource.Stop();
    }
}
