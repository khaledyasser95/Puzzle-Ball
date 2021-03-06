﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSound : MonoBehaviour {
    private Rigidbody myBody;
    private BallScript ballMovement;

    // AUDIO SOURCE PLAY 
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource ballRollAudio;
    [SerializeField]
    private AudioClip pickUp,wallHit;

    private Vector3 velocityLastFrame;
    private Vector3 collisionNormal;
    private float xAxisAngle, xFactor;
    private float yAxisAngle, yFactor;
    private float zAxisAngle, zFactor;


    // Use this for initialization
    void Awake () {
        myBody = GetComponent<Rigidbody>();
        ballMovement = GetComponent<BallScript>();
	}
	
	// Update is called once per frame
	void Update () {
        BallRollSoundController();

    }

    void LateUpdate()
    {
        velocityLastFrame = myBody.velocity;

    }
    public void BallRollSoundController()
    {
        if (ballMovement.onFloorTracker > 0 && myBody.velocity.sqrMagnitude >0)
        {
            ballRollAudio.volume = myBody.velocity.sqrMagnitude * 0.0002f;
            ballRollAudio.pitch = 0.4f + ballRollAudio.volume;
            ballRollAudio.mute = false;

        }else
        {
         //   ballRollAudio.mute = true;
        }

    }
    public void PlayPickUpSound()
    {
        audioSource.volume = 0.7f;
        audioSource.PlayOneShot(pickUp);



      }

    void SetSoundVolumeOnCollision(Collision target)
    {
        // Contact : array of contact point 
        //Contact point is a point where two colliders collided
        // collision normal 
        collisionNormal = target.contacts[0].normal;

        xAxisAngle = Vector3.Angle(Vector3.right, collisionNormal);
        xFactor = (1.0f / 8100f) * xAxisAngle * xAxisAngle + (-1 / 45f) + 1f;

        yAxisAngle = Vector3.Angle(Vector3.up, collisionNormal);
        yFactor = (1.0f / 8100f) * yAxisAngle * yAxisAngle + (-1 / 45f) + 1f;

        zAxisAngle = Vector3.Angle(Vector3.forward, collisionNormal);
        zFactor = (1.0f / 8100f) * zAxisAngle * zAxisAngle + (-1 / 45f) + 1f;

        audioSource.volume = (Mathf.Abs(velocityLastFrame.x) * xFactor * 0.001f) + (Mathf.Abs(velocityLastFrame.y * yFactor * 0.001f)) + (Mathf.Abs(velocityLastFrame.z) * zFactor * 0.001f);

    }
    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Wall")
        {
            SetSoundVolumeOnCollision(target);
            audioSource.PlayOneShot(wallHit);
        }
    }
}//class


















