using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerMovement PlayerMovement;
    AnimatorManager AnimatorManager;
    PlayerManger playerManger;
    Animator animator;

    QuestHandler Quest;

    [Header("Character weapon")]
    private GameObject ItemWeapon;
    private GameObject ItemWeapon2;
    private GameObject ItemWeapon4;
    private GameObject ItemWeapon5;

    BoxCollider WeaponBoxCollider;
    BoxCollider WeaponBoxCollider2;
    BoxCollider WeaponBoxCollider3;
    BoxCollider WeaponBoxCollider4;

    [Header("Character Point")]
    public GameObject PointHandRight;
    public GameObject PointHandRightSwordPos;
    public GameObject PointHandRight2;//Slot for short sword
    public GameObject PointHandLeft;
    public GameObject PointShieldHand;
    [Header("Holstered Slots")]
    public GameObject BackSlot;
    public GameObject BackSlotLeft;
    public GameObject BackSlotRight;
    public GameObject leftHip;
    public GameObject rightHip;

    private gameManager gamemanager;
    //[HideInInspector]
    public int WeaponState = 0; //Weapon State is used to determine which locomotion is active.
    private int ComboState = 0; //combo state works to see what attack will play.

    [Header("Movement Controls")]
    //movementInput is the x and y axis for player movement, set to public can see the change in unity engine.
    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float MoveAmount; //set to public so the playermovement can use move amount 

    //Camera inputs
    [Header("Camera Controls")]
    public Vector2 cameraInput;
    public float cameraInputX;
    public float cameraInputY;

    //button inputs bools
    public bool leftStickIn; //button bool to check for sprinting is true;

    [Header("Attack Controls")]
    public bool rtBtn; //attack for right trigger or left mouse click for attack
    public bool ltBtn; //block for left trigger or ctrl  

    public bool rtShoudlerBtn; //second attack with right shoulder button and right mouse for block
    public bool ltShoudlerBtn; 

    public bool JumpBtn; //jump for south button (A button) or Space bar
    public bool jumpInput;

    public bool DodgeBtn; //dodge move for east button (B button) or ctrl (unsure)??
    
    //Weapon Draw Inputs
    [SerializeField] private bool HolsterWeapons; //1 key,  left on D-pad
    [SerializeField] private bool WeaponDraw2; // 2 key, UP on D-pad
    [SerializeField] private bool WeaponDraw3; // 3 key, right on D-pad
    [SerializeField] private bool WeaponDraw4; // 4 key, down on D-pad

    private bool goalTab;

    //combo 
    public bool comboCheck;
    //private bool specialAttack;

    //Lock On 
    private bool lockOnTarget;
    public Transform target;

    private void Awake()
    {
        //gets all components 
        AnimatorManager = GetComponent<AnimatorManager>();
    
        PlayerMovement = GetComponent<PlayerMovement>();
      
        animator = GetComponent<Animator>();
        gamemanager = FindObjectOfType<gameManager>();
        Quest = FindObjectOfType<QuestHandler>();
        Quest.OpenGameGoals();

        if (gamemanager == null) print("No game manager in scene"); //if no manager is in scene show error message

        SpawnWeapon();

        

       
    }
    

    private void OnEnable()
    {
        //Sets the player inputs that was setup from the unity
        //player input system for both keyboard and controller
        //this allows for easy use of the inputs without having to write each input out.
        //instead this makes it just call the name of the input. 
        if(playerInput == null)
        {
            playerInput = new PlayerInput();

            playerInput.Player.move.performed += i => movementInput = i.ReadValue<Vector2>();
            //this will send input information for the mouse or rightStick to the camera input
            playerInput.Player.look.performed += i => cameraInput = i.ReadValue<Vector2>();

            //When run input is performed set left stick in true.  
            playerInput.Player.run.performed += i => leftStickIn = true;
            playerInput.Player.run.canceled += i => leftStickIn = false;

            
            //more inputs for later code 
          
            //block input
            playerInput.Player.block.performed += i => ltBtn = true;
            playerInput.Player.block.canceled += i => ltBtn = false;
            

            //jump input
            playerInput.Player.jump.performed += i => JumpBtn = true;
            playerInput.Player.jump.canceled += i => JumpBtn = false;

            //attack input

            //Weapon draws and changes animations from the input pressed
            //when input button is pressed set boolen to true or false.
            playerInput.Player.HeavyAttack.performed += i => rtBtn = true;
            playerInput.Player.HeavyAttack.canceled += i => rtBtn = false;

            playerInput.Player.Attack.performed += i => rtShoudlerBtn = true;
            playerInput.Player.Attack.canceled += i => rtShoudlerBtn = false;


            //State inputs
            playerInput.Player.HolsterWeapons.performed += i => HolsterWeapons = true;
            playerInput.Player.HolsterWeapons.canceled += i => HolsterWeapons = false;

            playerInput.Player.DrawWeapon2.performed += i => WeaponDraw2 = true;
            playerInput.Player.DrawWeapon2.canceled += i => WeaponDraw2 = false;

            playerInput.Player.DrawWeapon3.performed += i => WeaponDraw3 = true;
            playerInput.Player.DrawWeapon3.canceled += i => WeaponDraw3 = false;

            playerInput.Player.DrawWeapon4.performed += i => WeaponDraw4 = true;
            playerInput.Player.DrawWeapon4.canceled += i => WeaponDraw4 = false;

            //playerInput.Player.SpeicalAttack.performed += i => specialAttack = true;
            //playerInput.Player.SpeicalAttack.canceled += i => specialAttack = false;

            //add Lock On to be caps and left tigger L1 or L2
            //right stick in
            playerInput.Player.LockOn.performed += i => lockOnTarget = true;
            playerInput.Player.LockOn.canceled += i => lockOnTarget = false;

            //When player input is key is down it will set true and when its up it will set false

            playerInput.Player.GoalTab.performed += i => goalTab = true;
            playerInput.Player.GoalTab.canceled += i => goalTab = false;


        }

        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }


    private void MovementInput()
    {
        //handles the movement input
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
        

        //handles the camera input movements
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;


        //works out the moveAmount for the animations to work, abs makes them postive.
        MoveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        AnimatorManager.UpdateAnimtor(0, MoveAmount, PlayerMovement.playerRun);
        


    }

    private void RunInput()
    {
        if (leftStickIn && MoveAmount > 0.5f)
        {
            PlayerMovement.playerRun = true;

        }
        else 
        { PlayerMovement.playerRun = false; }
    }

    private void JumpInput() { 

        if (JumpBtn)
        {
            JumpBtn = false;
            PlayerMovement.Jump();
      
            
        }
    }

    private void AttackInput()
    {

        
        if (rtBtn && WeaponState != 0)
        {
            PlayerMovement.canMove = false;
            print("Attack"); //testing for attack input prints on click
            animator.SetTrigger("HeavyAttackT");
           
        }

        
        //if button press then if the combo == 1 run second attack else if combo state is 0,
        //set combo state to 1 and trigger a first attack.
       
        if (rtShoudlerBtn && WeaponState != 0)
        {
            PlayerMovement.canMove = false;
            if (ComboState == 1)
            {
                ComboState = 0;
                print("attackLight"); //testing for attack input prints on click
                animator.SetTrigger("AttackTrigger");
                animator.SetInteger("ComboNumber", 0);
                print("combo reset: " + (animator.GetInteger("ComboNumber")));
            }
            else if (ComboState == 0)
            { 
                ComboState = 1;
                print("Second Attack");
                animator.SetTrigger("AttackTrigger");
                animator.SetInteger("ComboNumber", 1);
                print("combo: " + (animator.GetInteger("ComboNumber")));
                
               
            }
  

        }

    }

    private void BlockInput()
    {
        if (ltBtn)
        {

            //print("Block Ctrl True");//testing block 
            //animator.SetTrigger("BlockState");
            animator.SetBool("Blocking", true);
            print(animator.GetBool("Blocking"));
            //add set bool for block animations and the correct state

        }
        else animator.SetBool("Blocking", false);

    }

    private void ActionInput()
    {
        if(goalTab)
        {
            Quest.GoalWindow.SetActive(true);

        }
        else
            Quest.GoalWindow.SetActive(false);
    }

     void SpawnWeapon()//Spawn weapon objects
    {
        ItemWeapon = Instantiate(gamemanager.meleeWeapon.GetComponent<Weapons>().weaponsArray[0]);//Sword
        ItemWeapon2 = Instantiate(gamemanager.meleeWeapon.GetComponent<Weapons>().weaponsArray[1]);//Shield 
        ItemWeapon4 = Instantiate(gamemanager.meleeWeapon.GetComponent<Weapons>().weaponsArray[3]);//handAxe
        ItemWeapon5 = Instantiate(gamemanager.meleeWeapon.GetComponent<Weapons>().weaponsArray[4]);//ShortSword

        //gets box collider to be called later and names weapon box collider 1 - 4, for the player.
        WeaponBoxCollider = ItemWeapon.GetComponent<BoxCollider>();
        WeaponBoxCollider2 = ItemWeapon2.GetComponent<BoxCollider>();
        WeaponBoxCollider3 = ItemWeapon4.GetComponent<BoxCollider>();
        WeaponBoxCollider4 = ItemWeapon5.GetComponent<BoxCollider>();

        //sets the box collider to false
        WeaponBoxCollider.enabled = false; //Sword
        WeaponBoxCollider2.enabled = false; //Shield
        WeaponBoxCollider3.enabled = false; //HandAxe
        WeaponBoxCollider4.enabled = false; //shortSword 

        WeaponHolster();//This function is called to put the spawn weapons into the correct positions in the game
        
    }

    private void WeaponHolster()
    {
        //When the weaponstate is zero all weapon objects are holstered this is called at Awake.
        //Input from user can equitment the weapon into the players hand.
        //this is done by changing the weapon state which changes it position to the correct slot
        if (WeaponState == 0) //All weapons being holstered
        {


            //Sword
            ItemWeapon.transform.parent = BackSlotRight.transform;
            ItemWeapon.transform.localPosition = Vector3.zero;
            ItemWeapon.transform.localEulerAngles = Vector3.zero;

            //Shield
            ItemWeapon2.transform.parent = BackSlot.transform;
            ItemWeapon2.transform.localPosition = Vector3.zero;
            ItemWeapon2.transform.localEulerAngles = Vector3.zero;
            //HandAxe
            ItemWeapon4.transform.parent = leftHip.transform;
            ItemWeapon4.transform.localPosition = Vector3.zero;
            ItemWeapon4.transform.localEulerAngles = Vector3.zero;

            //ShortSword
            ItemWeapon5.transform.parent = rightHip.transform;
            ItemWeapon5.transform.localPosition = Vector3.zero;
            ItemWeapon5.transform.localEulerAngles = Vector3.zero;


        }
        else if (WeaponState == 2)//Sheild and Sword actions
        {
            ItemWeapon.transform.parent = PointHandRightSwordPos.transform;
            ItemWeapon.transform.localPosition = Vector3.zero;
            ItemWeapon.transform.localEulerAngles = Vector3.zero;

            //shieldSlot is used as a child to pointhandLeft due to the position of the shield on the hand.
            ItemWeapon2.transform.parent = PointShieldHand.transform;
            ItemWeapon2.transform.localPosition = Vector3.zero;
            ItemWeapon2.transform.localEulerAngles = Vector3.zero;


            //HandAxe
            ItemWeapon4.transform.parent = leftHip.transform;
            ItemWeapon4.transform.localPosition = Vector3.zero;
            ItemWeapon4.transform.localEulerAngles = Vector3.zero;

            //ShortSword
            ItemWeapon5.transform.parent = rightHip.transform;
            ItemWeapon5.transform.localPosition = Vector3.zero;
            ItemWeapon5.transform.localEulerAngles = Vector3.zero;

        }
        else if (WeaponState == 3)//Duel Weapons actions
        {

            ItemWeapon4.transform.parent = PointHandLeft.transform;
            ItemWeapon4.transform.localPosition = Vector3.zero;
            ItemWeapon4.transform.localEulerAngles = Vector3.zero;

            ItemWeapon5.transform.parent = PointHandRight2.transform;
            ItemWeapon5.transform.localPosition = Vector3.zero;
            ItemWeapon5.transform.localEulerAngles = Vector3.zero;

            //Sword
            ItemWeapon.transform.parent = BackSlotRight.transform;
            ItemWeapon.transform.localPosition = Vector3.zero;
            ItemWeapon.transform.localEulerAngles = Vector3.zero;

            //Shield
            ItemWeapon2.transform.parent = BackSlot.transform;
            ItemWeapon2.transform.localPosition = Vector3.zero;
            ItemWeapon2.transform.localEulerAngles = Vector3.zero;

        }
        else if (WeaponState == 4)//Katana Actions
        {
          
            ItemWeapon5.transform.parent = PointHandRight2.transform;
            ItemWeapon5.transform.localPosition = Vector3.zero;
            ItemWeapon5.transform.localEulerAngles = Vector3.zero;

            //Axe
            ItemWeapon4.transform.parent = leftHip.transform;
            ItemWeapon4.transform.localPosition = Vector3.zero;
            ItemWeapon4.transform.localEulerAngles = Vector3.zero;

            //Sword
            ItemWeapon.transform.parent = BackSlotRight.transform;
            ItemWeapon.transform.localPosition = Vector3.zero;
            ItemWeapon.transform.localEulerAngles = Vector3.zero;

            //Shield
            ItemWeapon2.transform.parent = BackSlot.transform;
            ItemWeapon2.transform.localPosition = Vector3.zero;
            ItemWeapon2.transform.localEulerAngles = Vector3.zero;

        }

    }

    private void WeaponSelect()
    {
        //WeaponSelect funcation is used for player input to change
        //the weapon selected and the animations state
        if (HolsterWeapons){  //No Weapons

            print("Button press true");

            WeaponState = 0;
            animator.SetInteger("State", 0);
            WeaponHolster();
            //sets the button to false, over wise the weaponstate will be locked.
            HolsterWeapons = false;
        } 
        if (WeaponDraw2) //Sword and shield
        {
            WeaponState = 2;
            animator.SetInteger("State", 2);
            WeaponHolster();
            WeaponDraw2 = false;
        }
       
        if (WeaponDraw3)//dual weapons
        {
            WeaponState = 3;
            animator.SetInteger("State", 3);
            WeaponHolster();
            WeaponDraw3 = false;
        }

        if (WeaponDraw4) //Katanna
        {
            // WeaponState = 4;
            SetState(4);
            animator.SetInteger("State", 4);
            WeaponHolster();
            WeaponDraw4 = false;
        }
       
           
            
        
        
      
    }

    public void SetState(int num)
    {
        WeaponState = num;

    }

    public void CollisionStart()
    {
        //becuase there are different weapons this setup only calls the collider for the active state or weapon being used.
        //if Weaponstate ==  2 sword and sheild Collider 
        if (WeaponState == 2)
        {
            WeaponBoxCollider.enabled = true;

            WeaponBoxCollider2.enabled = true;
        }
        if (WeaponState == 3)
        {
            //if weapon state == 3 dual short sword and hand axe

            WeaponBoxCollider3.enabled = true;

            WeaponBoxCollider4.enabled = true;
        }
        //if weapon state == 4 kantana
        if (WeaponState == 4)
        {
            print("kantana box is on");
            WeaponBoxCollider4.enabled = true;
        }
    }
    public void CollisionEnd()
    {
        WeaponBoxCollider.enabled = false;

        WeaponBoxCollider2.enabled = false;

        WeaponBoxCollider3.enabled = false;

        WeaponBoxCollider4.enabled = false;
    }

    public void CollisonBlockStart()
    {
        WeaponBoxCollider2.enabled = true;
    }

    public void CollisonBlockEnd()
    {
        WeaponBoxCollider2.enabled = false;
    }


    private void LockOn()
    {
        if (lockOnTarget == true)
        {
            //look at the Enemy target. 
           
        }
    }






    public void Inputs() //all input funcations are stored and called from input when that is called.
    {
        MovementInput();
        RunInput();
        JumpInput();
        ActionInput();
        AttackInput();
        BlockInput();
        WeaponSelect();
    }


}
