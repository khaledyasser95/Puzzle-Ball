using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEnemyScript : MonoBehaviour {


    private Transform ballTarget;
    private Vector3 ballPositionDirection;

    private bool canAttack, readyToAttack;

    [HideInInspector]
    public bool stunned;

    private Rigidbody mybody;
    private RaycastHit ballHit;
	// Use this for initialization
	void Awake () {
        mybody = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
        CheckIfCanAttack();
	}

    void FixedUpdate()
    {
        Attack();
    }
    void GetBallTarget(Transform target) {

        ballTarget = target; 

    }

    void CanAttackToggle(bool canAttack)
    {
        this.canAttack = canAttack;
    }

    void CheckIfCanAttack()
    {
        if (canAttack && !stunned && (mybody.velocity.sqrMagnitude <= 0.11f))
        {
            ballPositionDirection = ballTarget.position - transform.position;
            if (Physics.Raycast(transform.position, ballPositionDirection, out ballHit, 25))
            {
                if (ballHit.transform.tag == "Ball")
                {
                    readyToAttack = true;
                }

            }
        }
    }
    void Attack()
    {
        if (readyToAttack)
        {
            mybody.AddForce(ballPositionDirection * 200f);
            readyToAttack = false;

        }
    }
}//classs





























