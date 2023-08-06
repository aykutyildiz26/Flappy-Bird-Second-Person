using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{  
    private CharacterController controller;
    private Vector3 direction;
    [Header("Player Movement Settings")]
    public float forwardSpeed;
    public float slideSpeed;
    public float laneDistance = 4; //Distance between 2 lanes.
    public static int desiredLane = 1; //0:LeftSide 1:MiddleSide 2:RightSide
    public SoundManager SoundManagerScript;

    void Start()
    {
        desiredLane = 1;
        SoundManagerScript = GameObject.Find("Sound Manager").GetComponent<SoundManager>();
        controller = GetComponent<CharacterController>();
        //transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        //Debug.Log(desiredLane);
        direction.z = forwardSpeed;
        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
            //Debug.Log(desiredLane);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            desiredLane--;
            if(desiredLane == -1)
                desiredLane = 0;
            //Debug.Log(desiredLane);
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y  * transform.up;
        /*
        if(desiredLane == 0)
        {
            //targetPosition += Vector3.left * laneDistance;
            targetPosition += new Vector3((-3.70f - laneDistance), 0, 0);
        }
        else if (desiredLane ==1)
        {
            //Center is "-2.81"
            targetPosition += new Vector3(-2.70f, 0,0);
        }
        else if(desiredLane == 2)
        {
            //targetPosition += Vector3.right * laneDistance;
            targetPosition += new Vector3((-1.70f + laneDistance), 0, 0);
        }
                Same as Switch-Case just like next codes.
                        --- It's working btw. ---
        */      
        switch (desiredLane)
        {
            case 0: targetPosition += new Vector3((-3.70f - laneDistance), 0, 0); ; break;
            case 1: targetPosition += new Vector3(-2.70f, 0,0); break;
            case 2: targetPosition += new Vector3((-1.70f + laneDistance), 0, 0); break;
        }

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * slideSpeed * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }



    /*
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            Handheld.Vibrate();
            
            SoundManagerScript.audioSource.PlayOneShot(SoundManagerScript.hitObstacle, 0.5f);
            PlayerManager.gameOver = true;
        }
    }
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Obstacle")
        {
            Handheld.Vibrate();

            SoundManagerScript.audioSource.PlayOneShot(SoundManagerScript.hitObstacle, 0.5f);
            PlayerManager.gameOver = true;
        }
    }
}
