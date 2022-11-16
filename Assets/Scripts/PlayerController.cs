using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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
    public  bool    canDash = true;           // Whether or not the player can dash
    public  bool    dashNeedsReset = false;   // Whether or not the player has used all their dashes

    //     SHOOTING VARIABLES      //
    [Header("Shooting Values")]
    public GameObject projectilePrefab;       // The prefab that will be shot by the player
    public int numOfShots = 3;         // How many shots the player has before needing to reload
    public float shotsCooldown = 5f;     // How long it takes for the player to reload
    public bool shotsNeedReset = false; // Whether or not the player has 0 shots

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
            // Gather input for jumping
            if (Input.GetKeyDown(KeyCode.Space) && numOfJumps > 0)
            {
                // Set this bool as true for FixedUpdate to see the input and execute the physics
                playerJumped = true;
                numOfJumps--;
            }

            // Gather input for dashing
            if (Input.GetKeyDown(KeyCode.LeftShift) && numOfDashes > 0 && canDash)
            {
                controller.useGravity = false;
                numOfDashes--;

                // Set this bool for lower in this method (Update) to recognize that the player needs to recover dashes
                if(numOfDashes == 0)
                {
                    Debug.Log("Dash Empty");
                    dashNeedsReset = true;
                }

                DoDash();
            }

            // Gather input for shooting
            if (Input.GetKeyDown(KeyCode.J) && stickDirection.magnitude != 0 && stickDirection.y >= 0 && numOfShots > 0)
            {
                // Create a projectile at the player in the direction the stick is pointing
                Instantiate(projectilePrefab, transform.position + stickDirection, Quaternion.LookRotation(stickDirection));
                numOfShots--;

                // Set this bool for lower in this method (Update) to recognize that the player needs to reload
                if (numOfShots == 0)
                {
                    Debug.Log("Shots Empty");
                    shotsNeedReset = true;
                }
            }

            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }

        // Runs if earlier in the update method the bool was set to initiate a dash reset
        if (dashNeedsReset)
        {
            Debug.Log("Resetting Dash");
            dashNeedsReset = false;                    // Prevents this block of code from being rerun and having multiple resets
            Invoke(nameof(ResetDash), dashCooldown);   // Does the dash reset after the cooldown
        }

        // Runs if earlier in the update method the bool was set to initaite a reload
        if (shotsNeedReset)
        {
            Debug.Log("Resetting Shots");
            shotsNeedReset = false;                    // Prevents this block of code from being rerun and having multiple reloads
            Invoke(nameof(ResetShots), shotsCooldown); // Does the relaod after the cooldown
        }
    }

    void FixedUpdate()
    {
        // Turns the stick input into a vector on the x and y axis
        stickDirection = new Vector3(horizontal, vertical, 0f).normalized;

        // Takes the left and right input from the stick but ignores the y to prevent making the player "jump" with the stick
        moveDirection = new Vector3(horizontal, 0f, 0f);

        // Shows the direction the stick is currently pointed towards
        Debug.DrawRay(transform.position, stickDirection*5, Color.red);

        // If the stick is being moved at all
        if (stickDirection.magnitude >= 0.1f)
        {
            // Move the player left or right
            controller.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
        }

        RotatePlayer();

        // If the update method registered a jump input execute a jump
        if (playerJumped == true)
        {
            controller.velocity = Vector3.zero;
            controller.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            playerJumped = false;
        }
    }

    // When the player collides with anything tagged as a floor they regain their jumps
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            numOfJumps = 2;
        }
    }

    // Execute a physics based dash using the values set in the inspector
    void DoDash()
    {
        canDash = false;
        controller.velocity = Vector3.zero;
        controller.AddForce(stickDirection * dashSpeed * Time.fixedDeltaTime * 100, ForceMode.Impulse);
        
        Invoke(nameof(StopDash), dashLength);
    }

    // Stop the dash and re-enable gravity for the player
    void StopDash()
    {
        controller.velocity = Vector3.zero;
        controller.useGravity = true;
        canDash = true;
    }

    // Replenish the amount of dashes that the player can do
    void ResetDash()
    {
        numOfDashes = 2;
        Debug.Log("Dash Reset");
    }

    // Replenish the amount of shots the player has
    void ResetShots()
    {
        numOfShots = 3;
        Debug.Log("Shots Reset");
    }

    // Move the player to the left or to the right depending on what direction they are moving in
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

        // If the player is not moving the character faces the camera
        if (moveDirection.x == 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 180, 0), 0.2f);
        }
    }
}
