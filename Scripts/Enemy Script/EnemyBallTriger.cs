using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyBallTriger : MonoBehaviour {
    private Rigidbody mybody;

    // AUDIO SOURCE PLAY 
    [SerializeField]
    private AudioSource ballRollAudio, audioSource, stunnedAudio;

    [SerializeField]
    private AudioClip wallHit, stunnedClip;
    private Vector3 velocityLastFrame;
    private Vector3 collisionNormal;
    private float xAxisAngle, xFactor;
    private float yAxisAngle, yFactor;
    private float zAxisAngle, zFactor;

    private BallEnemyScript enemyBall;
    private MeshRenderer Renderer;

    // Use this for initialization
    void Awake () {
        mybody = GetComponent<Rigidbody>();
        enemyBall = GetComponent<BallEnemyScript>();
        Renderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        BallRollSoundController();

    }
    void LateUpdate()
    {
        velocityLastFrame = mybody.velocity;

    }
    public void BallRollSoundController()
    {
        if (mybody.velocity.sqrMagnitude > 0)
        {
            ballRollAudio.volume = mybody.velocity.sqrMagnitude * 0.0002f;
            ballRollAudio.pitch = 0.4f + ballRollAudio.volume;
            ballRollAudio.mute = false;

        }
        else
        {
            ballRollAudio.mute = true;
        }

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

            if (!enemyBall.stunned && (Mathf.Abs(velocityLastFrame.x) * xFactor + Mathf.Abs(velocityLastFrame.y) * yFactor + Mathf.Abs(velocityLastFrame.z) * zFactor)>15f) 
            {
                enemyBall.stunned = true;
                Renderer.material.color = Color.yellow;
                stunnedAudio.PlayOneShot(stunnedClip);
                StartCoroutine(BallStunned());
            }
          
        }
        if (target.gameObject.tag == "Ball")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    IEnumerator BallStunned()
    {
        yield return new WaitForSeconds(2f);
        Renderer.material.color = Color.blue;
        enemyBall.stunned = false;
    }

    //SHOULD WE ATTACK OR NOT
    void OnTriggerEnter(Collider target)
    {
         if (target.tag == "Ball")
        {
            gameObject.SendMessage("GetBallTarget", target.transform);
            gameObject.SendMessage("CanAttackToggle", true);
        }
    }

    void OnTriggerExit(Collider target)
    {
        if (target.tag == "Ball")
        {
            gameObject.SendMessage("GetBallTarget", target.transform);
            gameObject.SendMessage("CanAttackToggle", false);
        }
    }
}
