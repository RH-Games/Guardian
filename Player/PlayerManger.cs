using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManger : MonoBehaviour
{
    //this script is used to run all funcations for our character

    InputManager inputManager;
    PlayerMovement playerMovement;
    CameraSettings cameraSettings;
    gameManager gamemanager;
    Animator animator;

    [SerializeField] ParticleSystem particleBlood;
    [SerializeField] ParticleSystem particleBlood2;

    public bool isPlaying;
    public bool combo;

    [Header("Player Health")]
    public int PlayerHealth;
    public int PlayerHealthMax = 100;
    public HealthBar healthBar;
    

    private void Awake()
    {
        //gets component thats on player object
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
        //Get object from camera settings 
        cameraSettings = FindObjectOfType<CameraSettings>();
        gamemanager = FindObjectOfType<gameManager>();
        animator = GetComponent<Animator>();
        PlayerHealth = PlayerHealthMax;
        healthBar.SetMaxHealth(PlayerHealthMax);

    }
    
    //runs every frame
    void Update()
    {
        
        inputManager.Inputs();
       
    }

    private void FixedUpdate()
    {
        playerMovement.Movement();

    }

    //calls it after the frame has ended
    private void LateUpdate()
    {
        cameraSettings.CameraMovement();

       // isPlaying = animator.GetBool("isPlaying");

        // playerMovement.playerJumping = animator.GetBool("playerJumping");
        //animator.SetBool("playerGrounded", playerMovement.onGround);

        inputManager.rtBtn = false;
        inputManager.rtShoudlerBtn = false;

    }

    public void PlayerDamage(int dmg)
    {

        if (animator.GetBool("Blocking") == true)
        {
            PlayerHealth = PlayerHealth - 0;
            healthBar.SetHealth(PlayerHealth);

        }
        else
        {
            PlayerHealth = PlayerHealth - dmg;
            healthBar.SetHealth(PlayerHealth);
            particleBlood.Play();
        }

        if (PlayerHealth <= 0)
        {
            particleBlood2.Play();
            PlayerDead();
        }
    }

    public void PlayerDead()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<InputManager>().enabled = false;
        gamemanager.GameOverLost();

    }


}
