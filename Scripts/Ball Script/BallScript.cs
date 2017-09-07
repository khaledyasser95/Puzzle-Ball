using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BallScript : MonoBehaviour {
    private string direction = "";
    private string directionLastFrame = "";

    [HideInInspector] 
    public int onFloorTracker = 0;

    private bool fullSpeed;

    //Speed Variables
    private int floorSpeed = 100;
    private int airSpeed = 20;
    private float airSpeed_Diagonal = 5.858f;
    private float air_drag = 0.1f;
    private float floorDrag = 2.29f;
    private float delta = 50f;

    //Camera Variables
    private Vector3 cameraRelative_Right;
    private Vector3 cameraRelative_Up;
    private Vector3 cameraRelative_Up_Left;
    private Vector3 cameraRelative_Up_Right;

    //Velocity and Magnitude
    private Vector3 x_Vel;
    private Vector3 y_Vel;
    private Vector3 z_Vel;

    private float x_Speed;
    private float z_Speed;

    //Movement Axis
    private string Axis_Y = "Vertical";
    private string Axis_X = "Horizontal";

    private Rigidbody mybody;

    private Camera mainCamera;

    void Awake()
    {
        mybody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetDirection();
        UpdateCameraRelativePosition();
        FullSpeedController();
        DragAdjustmentAndAirSpeed();
        BallFellDown();
    }

    void LateUpdate()
    {
        directionLastFrame = direction;
    }
    void FixedUpdate()
    {
        MoveTheBall();
    }

    void GetDirection()
    {
        direction = "";
        if (Input.GetAxis(Axis_Y) > 0)
            direction += "up";
        else if (Input.GetAxis(Axis_Y) < 0)
            direction+="down";

        if (Input.GetAxis(Axis_X) > 0)
            direction += "right";
        else if (Input.GetAxis(Axis_X) < 0)
            direction += "left";

    }

   
    void MoveTheBall()
    {
        switch (direction)
        {

            case "upright":

                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        mybody.AddForce(floorSpeed * cameraRelative_Up_Right *
                            Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        mybody.AddForce((floorSpeed - 75f) * cameraRelative_Up_Right *
                            Time.fixedDeltaTime * delta);
                    }

                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_Vel.normalized == cameraRelative_Up)
                    {
                        if (z_Speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            mybody.AddForce(10.6f * cameraRelative_Up *
                            Time.fixedDeltaTime * delta);
                        }
                    }
                    else
                    {
                        mybody.AddForce(10.6f * cameraRelative_Up *
                            Time.fixedDeltaTime * delta);
                    }

                    if (x_Vel.normalized == cameraRelative_Right)
                    {
                        if (x_Speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            mybody.AddForce(10.6f * cameraRelative_Right *
                                Time.fixedDeltaTime * delta);
                        }
                    }
                    else
                    {
                        mybody.AddForce(10.6f * cameraRelative_Right *
                            Time.fixedDeltaTime * delta);
                    }

                }

                break;

            case "upleft":

                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        mybody.AddForce(floorSpeed * cameraRelative_Up_Left *
                        Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        mybody.AddForce((floorSpeed - 75f) * cameraRelative_Up_Left *
                        Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_Vel.normalized == cameraRelative_Up)
                    {
                        if (z_Speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            mybody.AddForce(10.6f * cameraRelative_Up
                                * Time.fixedDeltaTime * delta);
                        }
                        else
                        {
                            mybody.AddForce(10.6f * cameraRelative_Up
                                * Time.fixedDeltaTime * delta);
                        }
                    }

                    if (x_Vel.normalized == -cameraRelative_Right)
                    {
                        if (x_Speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            mybody.AddForce(10.6f * -cameraRelative_Right *
                            Time.fixedDeltaTime * delta);
                        }
                    }
                    else
                    {
                        mybody.AddForce(10.6f * -cameraRelative_Right *
                            Time.fixedDeltaTime * delta);
                    }

                }

                break;

            case "downright":

                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        mybody.AddForce(floorSpeed * -cameraRelative_Up_Left *
                            Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        mybody.AddForce((floorSpeed - 75f) * -cameraRelative_Up_Left *
                            Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_Vel.normalized == -cameraRelative_Up)
                    {
                        if (z_Speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            mybody.AddForce(10.6f * -cameraRelative_Up
                                * Time.fixedDeltaTime * delta);
                        }
                        else
                        {
                            mybody.AddForce(10.6f * -cameraRelative_Up
                                * Time.fixedDeltaTime * delta);
                        }
                    }

                    if (x_Vel.normalized == cameraRelative_Right)
                    {
                        if (x_Speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            mybody.AddForce(10.6f * cameraRelative_Right *
                                Time.fixedDeltaTime * delta);
                        }
                    }
                    else
                    {
                        mybody.AddForce(10.6f * cameraRelative_Right *
                            Time.fixedDeltaTime * delta);
                    }

                }

                break;

            case "downleft":

                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        mybody.AddForce(floorSpeed * -cameraRelative_Up_Right *
                            Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        mybody.AddForce((floorSpeed - 75f) * -cameraRelative_Up_Right *
                            Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_Vel.normalized == -cameraRelative_Up)
                    {
                        if (z_Speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            mybody.AddForce(10.6f * -cameraRelative_Up
                                * Time.fixedDeltaTime * delta);
                        }
                        else
                        {
                            mybody.AddForce(10.6f * -cameraRelative_Up
                                * Time.fixedDeltaTime * delta);
                        }
                    }

                    if (x_Vel.normalized == -cameraRelative_Right)
                    {
                        if (x_Speed < (airSpeed - airSpeed_Diagonal - 0.1f))
                        {
                            mybody.AddForce(10.6f * -cameraRelative_Right *
                                Time.fixedDeltaTime * delta);
                        }
                    }
                    else
                    {
                        mybody.AddForce(10.6f * -cameraRelative_Right *
                            Time.fixedDeltaTime * delta);
                    }

                }

                break;

            case "up":

                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        mybody.AddForce(floorSpeed * cameraRelative_Up *
                            Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        mybody.AddForce((floorSpeed - 75f) * cameraRelative_Up *
                            Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_Speed < airSpeed)
                    {
                        mybody.AddForce((airSpeed * 0.75f) * cameraRelative_Up
                            * Time.fixedDeltaTime * delta);
                    }

                    if (x_Speed > 0.1f)
                    {
                        if (x_Vel.normalized == cameraRelative_Right)
                        {
                            mybody.AddForce((airSpeed * 0.75f) * -cameraRelative_Right
                                * Time.fixedDeltaTime * delta);
                        }
                        else if (x_Vel.normalized == -cameraRelative_Right)
                        {
                            mybody.AddForce((airSpeed * 0.75f) * cameraRelative_Right
                                * Time.fixedDeltaTime * delta);
                        }
                    }

                }

                break;

            case "down":

                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        mybody.AddForce(floorSpeed * -cameraRelative_Up *
                            Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        mybody.AddForce((floorSpeed - 75f) * -cameraRelative_Up *
                            Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (z_Speed < airSpeed)
                    {
                        mybody.AddForce((airSpeed * 0.75f) * -cameraRelative_Up
                            * Time.fixedDeltaTime * delta);
                    }

                    if (x_Speed > 0.1f)
                    {
                        if (x_Vel.normalized == cameraRelative_Right)
                        {
                            mybody.AddForce((airSpeed * 0.75f) * -cameraRelative_Right
                                * Time.fixedDeltaTime * delta);
                        }
                        else if (x_Vel.normalized == -cameraRelative_Right)
                        {
                            mybody.AddForce((airSpeed * 0.75f) * cameraRelative_Right
                                * Time.fixedDeltaTime * delta);
                        }
                    }

                }

                break;

            case "right":

                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        mybody.AddForce(floorSpeed * cameraRelative_Right *
                            Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        mybody.AddForce((floorSpeed - 75f) * cameraRelative_Right *
                            Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (x_Speed < airSpeed)
                    {
                        mybody.AddForce((airSpeed * 0.75f) * cameraRelative_Right
                            * Time.fixedDeltaTime * delta);
                    }

                    if (z_Speed > 0.1f)
                    {
                        if (z_Vel.normalized == cameraRelative_Up)
                        {
                            mybody.AddForce((airSpeed * 0.75f) * -cameraRelative_Up
                                * Time.fixedDeltaTime * delta);
                        }
                        else if (z_Vel.normalized == -cameraRelative_Up)
                        {
                            mybody.AddForce((airSpeed * 0.75f) * cameraRelative_Up
                                * Time.fixedDeltaTime * delta);
                        }
                    }

                }

                break;

            case "left":

                if (onFloorTracker > 0)
                {
                    // on floor
                    if (fullSpeed)
                    {
                        mybody.AddForce(floorSpeed * -cameraRelative_Right *
                            Time.fixedDeltaTime * delta);
                    }
                    else
                    {
                        mybody.AddForce((floorSpeed - 75f) * -cameraRelative_Right *
                            Time.fixedDeltaTime * delta);
                    }
                }
                else if (onFloorTracker == 0)
                {
                    // in air
                    if (x_Speed < airSpeed)
                    {
                        mybody.AddForce((airSpeed * 0.75f) * -cameraRelative_Right
                            * Time.fixedDeltaTime * delta);
                    }

                    if (z_Speed > 0.1f)
                    {
                        if (z_Vel.normalized == cameraRelative_Up)
                        {
                            mybody.AddForce((airSpeed * 0.75f) * -cameraRelative_Up
                                * Time.fixedDeltaTime * delta);
                        }
                        else if (z_Vel.normalized == -cameraRelative_Up)
                        {
                            mybody.AddForce((airSpeed * 0.75f) * cameraRelative_Up
                                * Time.fixedDeltaTime * delta);
                        }
                    }

                }

                break;

        }
    }
    void UpdateCameraRelativePosition()
    {/*
        You need to understand the difference between local space and world space. 
        Local is for directions and rotations relative to the object, and world is relative to the game world. 
        This function takes a local direction from an object and finds that direction in world space. 
        Vector3(1,0,0) is the same as Vector3.right, which in the object's local space is the direction pointing to the right of the object. 
        Depending on how the object is rotated, the output in world space will change.
    */
        cameraRelative_Right = mainCamera.transform.TransformDirection(Vector3.right);
        cameraRelative_Up = mainCamera.transform.TransformDirection(Vector3.forward);
        cameraRelative_Up.y = 0f;
        // Makes the Magnitude == 1 not more
        cameraRelative_Up = cameraRelative_Up.normalized;

        cameraRelative_Up_Right = (cameraRelative_Up + cameraRelative_Right);
        cameraRelative_Up_Right = cameraRelative_Up_Right.normalized;

        cameraRelative_Up_Left = (cameraRelative_Up - cameraRelative_Right);
        cameraRelative_Up_Left = cameraRelative_Up_Left.normalized;
    }

    void FullSpeedController()
    {
        if (direction != directionLastFrame)
        {
            if (direction == "")
            {
                StopCoroutine("FullSpeedTimer");
                fullSpeed = false;
            
            }else if (directionLastFrame == "")
            {
                StartCoroutine("FullSpeedTimer");
            }
        }
    }
    IEnumerator FullSpeedTimer()
    {
        yield return new WaitForSeconds(0.07f);
        fullSpeed = true;
    }

    void DragAdjustmentAndAirSpeed()
    {
        if (onFloorTracker > 0)
        {
            // On Floor
            mybody.drag = floorDrag;

        }else
        {
            x_Vel = Vector3.Project(mybody.velocity, cameraRelative_Right);
            z_Vel = Vector3.Project(mybody.velocity, cameraRelative_Up);

            x_Speed = x_Vel.magnitude;
            z_Speed = z_Vel.magnitude;
            mybody.drag = air_drag;

            
        }

    }
    void BallFellDown()
    {
        if (transform.position.y < -30)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Floor")
        {
            onFloorTracker++;
        }
    }
    void OnCollisionExit(Collision target)
    {
        if (target.gameObject.tag == "Floor")
        {
            onFloorTracker--;
        }
    }
}//class





































