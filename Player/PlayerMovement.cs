using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    PlayerManger playerManger;
    AnimatorManager animatorManager;
    Transform cameraObject;
    Rigidbody playerRb;

    Vector3 movePlayerdirection;

    [Header("Movement settings")]
    //these are public as we can change them in the inspector in unity 
    //floats number 0.0
    public float walkSpeed = 2; //walk speed
    public float moveSpeed = 5; //slow walk
    public float runSpeed = 7; //run speed 
    public float rotationSpeed = 12;

    public bool playerRun;
    
    [Header("Falling settings")]
    //Falling 
    public LayerMask groundLayer;
    public float airTime;
    public float JumpVelocity;
    public float FallSpeed;
    public float rayCastOffset = 0.5f;

    //starts on ground
    public bool onGround = true;
    public bool canMove;

    //jumping 
    public bool playerJumping;
    public float jumpHeight = 3.5f;
    public float GravityStrength = -9.81f;

  

    private void Awake()
    {
        //gets all components in awake as this goes before update and start.  
        inputManager = GetComponent<InputManager>();
        playerManger = GetComponent<PlayerManger>();
        animatorManager = GetComponent<AnimatorManager>();
        playerRb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        canMove = true;
    }

    private void HandlePlayerMovement()
    {
        //moves the player forward and back
        movePlayerdirection = cameraObject.forward * inputManager.verticalInput;
        //moves the player left and right
        movePlayerdirection = movePlayerdirection + cameraObject.right * inputManager.horizontalInput;
        movePlayerdirection.Normalize();//keeps direction the same and change length to 1.
        movePlayerdirection.y = 0; //sets the player y to zero making it move up y axis.
        //movePlayerdirection = movePlayerdirection * moveSpeed;

        if (playerRun)
        {
            movePlayerdirection *= runSpeed;
        }
        else //if player not running it will check the nested if statement below
        {

            //if the move amount is  less then  or equal to 0.2f use move speed else use walk speed
            if (inputManager.MoveAmount >= 0.2f)
            {
                movePlayerdirection *= moveSpeed;
            }
            else
            {
                movePlayerdirection *= walkSpeed;
            }
        }
        //will move player on based on move player  direction.
        Vector3 moveVelocity = movePlayerdirection;
        playerRb.velocity = moveVelocity;


    }

    private void HandlePlayerRotate()
    {
        //sets all vectors to zero at start 
        //player Direction is the direct the player will move when it rotates
        Vector3 playerDirection = Vector3.zero;

 

        playerDirection = cameraObject.forward * inputManager.verticalInput;
        playerDirection = playerDirection + cameraObject.right * inputManager.horizontalInput;
        playerDirection.Normalize();
        playerDirection.y = 0;

        if(playerDirection == Vector3.zero)
        {
            playerDirection = transform.forward;
        }

        //uses to caltuate rotations
        //this makes it to where the player is lookign is the directiong the player will rotate to.
        Quaternion RotateDirection = Quaternion.LookRotation(playerDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, RotateDirection, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void Movement()
    {
        FallingMovement();

        if (playerJumping) //stops the move and rotate funcations from being called
            return;
        //if isPlaying is true it won't run the movement or rotate player functions
        if (playerManger.isPlaying)
            return;

        //calls player movement and rotation funcations 
        HandlePlayerMovement();
        HandlePlayerRotate();
    }

    //this will check if the player is falling by using raycast and check if the player is on the ground.
    private void FallingMovement()
    {
        //is the posistion of the plays feet or bottom of the object
        Vector3 rayCastPos = transform.position;
        Vector3 PlayerPosition;
        Vector3 rayCastHit;
        RaycastHit hit;
        PlayerPosition = transform.position;


        rayCastPos.y = rayCastPos.y + rayCastOffset; ; //sets the position to just above the ground
                                                       //checks for the ground and sets ground true
        if (Physics.SphereCast(rayCastPos, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            //if not ground and not playing then play animation
            if (!onGround && !playerManger.isPlaying)
            {
                animatorManager.PlayAnimation("land", true);
            }

            rayCastHit = hit.point;
            PlayerPosition.y = rayCastHit.y;
            airTime = 0;
            onGround = true;
        }
        else
        {
            //is in air
            onGround = false;
        }

        //if not on the ground and not jumping 
        if (!onGround && !playerJumping)
        {   
            //if the animation is not playing play in air animation
            if (!playerManger.isPlaying)
            {
                animatorManager.PlayAnimation("inAir", true);
            }

            airTime = airTime + Time.deltaTime;
            playerRb.AddForce(transform.forward * JumpVelocity);
            playerRb.AddForce(-Vector3.up * FallSpeed * airTime);
        }

        if (onGround && !playerJumping)
        {
            //checks if the player is on the ground and not jumping make the player move up small amount
            //this will let the player move up slope objects or stairs as the collider as above his feet
            if(playerManger.isPlaying || inputManager.MoveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position, PlayerPosition, Time.deltaTime/0.01f);
            }
            else
            {
                transform.position = PlayerPosition;
            }
        }

       
    }

    public void Jump()
    {
        //check if the player is on ground, and if they are they can jump
        if (onGround)
        {
            //sets jump animation 
            animatorManager.animator.SetBool("PlayerGrounded", true);
            animatorManager.animator.SetBool("playerJumping", false);
            animatorManager.PlayAnimation("jump", false);

            //works out the jump velocity  ( -2 times -9.81 * 3.5 ) and squares it to get the velocity
            float jumpVelocity = Mathf.Sqrt(-2 * GravityStrength * jumpHeight);
            Vector3 playerVelocity = movePlayerdirection;
            playerVelocity.y = jumpVelocity;
            playerRb.velocity = playerVelocity; //sets the velocity to player velocity
        }
      

    }

}
