using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    ////////// VARIABLES ///////////

    //     GENERIC VARIABLES      //
    private Rigidbody controller;             // The Player Controller
    public  Vector3 stickDirection;           // The direction the stick is pushed towards
    private TrailRenderer trail;

    //     JUMPING VARIABLES      //
    [Header("Jumping Values")]
    public  bool     canJump = true;           // How many jumps the player can execute
    public  float   jumpStrength = 2f;        // How high the player can jump
    private bool    playerJumped = false;     // Whether or not the player inputted a jump
    public  bool    inTheAir = false;         // Whether or not the player is in the air

    //     DASHING VARIABLES      //
    [Header("Dashing Values")]
    public  int     numOfDashes = 1;          // How many dashes the player can execute
    public  float   dashLength = 0.3f;        // How long dash lasts
    public  float   dashSpeed = 10;           // Speed of dash
    public  bool    dashNeedsReset = false;   // Whether or not the player has used all their dashes

    //     SHOOTING VARIABLES      //
    [Header("Shooting Values")]
    public GameObject projectilePrefab;       // The prefab that will be shot by the player
    public int        numOfShots = 3;         // How many shots the player has before needing to reload
    public float      shotsCooldown = 5f;     // How long it takes for the player to reload
    public bool       shotsNeedReset = false; // Whether or not the player has 0 shots
    public Vector3    shootDirection;         // What direction the projectile will go

    //     MOVEMENT VARIABLES     //
    [Header("Movement Values")]
    public  bool    canMove = true;           // Whether or not the player can move
    public  float   speed = 6f;               // How fast the player can move
    private float   horizontal;               // Variable for horizontal input
    private float   vertical;                 // Variable for vertical input
    public  Vector3 moveDirection;            // The direction the player is moving

    //     ANIMATOR VARIABLES     //
    [Header("Animator Values")]
    public Animator anim;                                                            // Animator controller reference
    private int jumpHash = Animator.StringToHash("Jump");                            // The hashID of the jump trigger parameter in animator
    private int isGroundedHash = Animator.StringToHash("isGrounded");                // The hashID of the of the grounded boolean for the animator
    private int isWalkingHash = Animator.StringToHash("isWalking");                  // The hashID of the walking boolean for the animator
    private int dashHash = Animator.StringToHash("Dash");                            // The hashID of the dash trigger parameter in animator
    private int isWalkingBackwardHash = Animator.StringToHash("isWalkingBackward");  // The hashID of the walking boolean for the animator
    private int isWalkingForwardHash = Animator.StringToHash("isWalkingForward");    // The hashID of the walking boolean for the animator
    private int ThrowKunaiHash = Animator.StringToHash("ThrowKunai");

    /////////// METHODS ////////////

    private void Start()
    {
        controller = gameObject.GetComponent<Rigidbody>();
        trail = gameObject.GetComponent<TrailRenderer>();
        shootDirection = Vector3.right;
    }

    private void Update()
    {
        // Shows the direction the stick is currently pointed towards
        Debug.DrawRay(transform.position, stickDirection * 10, Color.red);
        // Shows the current shooting direction
        Debug.DrawRay(transform.position, shootDirection * 5, Color.blue);

        if (canMove)
        {
            // Gather input for jumping
            if (Input.GetKeyDown(KeyCode.Space) && inTheAir == false && canJump == true)
            {
                // Set this bool as true for FixedUpdate to see the input and execute the physics
                playerJumped = true;
                canJump = false;
            }

            // Gather input for dashing
            if (Input.GetKeyDown(KeyCode.Space) && inTheAir == true && numOfDashes > 0)
            {
                // Trigger Dash animation
                anim.SetTrigger(dashHash);

                controller.useGravity = false;
                numOfDashes--;

                DoDash();
            }

            // Gather input for shooting
            if (Input.GetKeyDown(KeyCode.E) && numOfShots > 0)
            {
                // Trigger animation for throwing kunai
                anim.SetTrigger(ThrowKunaiHash);

                // Create a projectile at the player in the direction the stick is pointing
                Instantiate(projectilePrefab, transform.position + shootDirection, Quaternion.LookRotation(shootDirection));
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

        // If the stick is being moved at all
        if (stickDirection.magnitude >= 0.1f)
        {
            // Move the player left or right
            controller.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);

            shootDirection = stickDirection;
        }

        // If the stick is NOT being moved at all
        else
        {
            // Set animator parameter for Walking
            anim.SetBool(isWalkingHash, false);
            shootDirection = Vector3.right;
        }

        AnimatePlayer();

        // If the update method registered a jump input execute a jump
        if (playerJumped == true)
        {
            // Trigger jump animation
            anim.SetTrigger(jumpHash);

            // Set animator parameter for ground check animations
            anim.SetBool(isGroundedHash, false);

            controller.velocity = Vector3.zero;
            controller.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            playerJumped = false;
        }
    }

    // When the player collides with anything tagged as a floor they regain their jump
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
            numOfDashes = 1;
            inTheAir = false;

            // Set animator parameter for ground check animations
            anim.SetBool(isGroundedHash, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            inTheAir = true;
            canJump = false;
        }
    }

    // Execute a physics based dash using the values set in the inspector
    void DoDash()
    {
        trail.startWidth = 1;
        trail.time = 1;
        
        controller.velocity = Vector3.zero;
        controller.AddForce(stickDirection * dashSpeed * Time.fixedDeltaTime * 100, ForceMode.Impulse);
        
        Invoke(nameof(StopDash), dashLength);
    }

    // Stop the dash and re-enable gravity for the player
    void StopDash()
    {
        trail.startWidth = 0.2f;
        trail.time = 0.5f;

        controller.velocity = Vector3.zero;
        controller.useGravity = true;
    }

    // Replenish the amount of shots the player has
    void ResetShots()
    {
        numOfShots = 3;
        Debug.Log("Shots Reset");
    }

    // Animate the player based on what direction they are moving in
    void AnimatePlayer()
    {
        if (moveDirection.x > 0)
        {
            // Set animator parameters for Walking Right
            anim.SetBool(isWalkingHash, true);
            anim.SetBool(isWalkingForwardHash, true);
            anim.SetBool(isWalkingBackwardHash, false);
        }

        if (moveDirection.x < 0)
        {
            // Set animator parameters for Walking Right
            anim.SetBool(isWalkingHash, true);
            anim.SetBool(isWalkingForwardHash, false);
            anim.SetBool(isWalkingBackwardHash, true);
        }

        if (moveDirection.x == 0)
        {
            // Set animator parameters for Walking Right
            anim.SetBool(isWalkingHash, false);
            anim.SetBool(isWalkingForwardHash, false);
            anim.SetBool(isWalkingBackwardHash, false);
        }
    }
}
