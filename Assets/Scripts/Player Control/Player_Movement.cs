using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    ////////// VARIABLES ///////////
    
    //        COMPONENTS          //
    private Rigidbody controller;             // The Player Controller

    //     JUMPING VARIABLES      //
    [Header("Jumping Values")]
    public  int     numOfJumps = 2;           // How many jumps the player can execute
    public  float   jumpStrength = 2f;        // How high the player can jump
    private bool    playerJumped = false;     // Whether or not the player inputted a jump
    
    //     DASHING VARIABLES      //
    [Header("Dashing Values")]
    public  int     numOfDashes = 2;          // How many dashes the player can execute
    public  float   dashLength = 0.3f;        // How long dash lasts
    public  float   dashCooldown = 5f;        // Time before new dash
    public  float   dashSpeed = 10;           // Speed of dash
    public  bool    canDash = true;
    public  bool    dashNeedsReset = false;

    //     MOVEMENT VARIABLES     //
    [Header("Movement Values")]
    public  bool    canMove = true;           // Whether or not the player can move
    public  float   speed = 6f;               // How fast the player can move
    private float   horizontal;               // Variable for horizontal input
    private float   vertical;                 // Variable for vertical input
    public  Vector3 stickDirection;           // The direction the stick is pushed towards
    public  Vector3 moveDirection;            // The direction the player is moving

    /////////// METHODS ////////////

    private void Start()
    {
        controller = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space) && numOfJumps > 0)
            {
                playerJumped = true;
                numOfJumps--;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && numOfDashes > 0 && canDash)
            {
                controller.useGravity = false;
                numOfDashes--;

                if(numOfDashes == 0)
                {
                    Debug.Log("Dash Empty");
                    dashNeedsReset = true;
                }

                DoDash();
            }

            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        if (dashNeedsReset)
        {
            Debug.Log("Resetting Dash");
            dashNeedsReset = false;
            Invoke(nameof(ResetDash), dashCooldown);
        }
    }

    void FixedUpdate()
    {
        stickDirection = new Vector3(horizontal, vertical, 0f).normalized;
        moveDirection = new Vector3(horizontal, 0f, 0f);
        Debug.DrawRay(transform.position, stickDirection*5, Color.red);

        if (stickDirection.magnitude >= 0.1f)
        {
            controller.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
        }

        RotatePlayer();

        if (playerJumped == true)
        {
            controller.velocity = Vector3.zero;
            controller.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            playerJumped = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            numOfJumps = 2;
        }
    }

    void DoDash()
    {
        canDash = false;
        controller.velocity = Vector3.zero;
        controller.AddForce(stickDirection * dashSpeed * Time.fixedDeltaTime * 100, ForceMode.Impulse);
        
        Invoke(nameof(StopDash), dashLength);
    }

    void StopDash()
    {
        controller.velocity = Vector3.zero;
        controller.useGravity = true;
        canDash = true;
    }

    void ResetDash()
    {
        numOfDashes = 2;
        Debug.Log("Dash Reset");
    }

    void RotatePlayer()
    {
        if (moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 120, 0), 0.2f);
        }

        if (moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 240, 0), 0.2f);
        }

        if (moveDirection.x == 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), 0.2f);
        }
    }
}
