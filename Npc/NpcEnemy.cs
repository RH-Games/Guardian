using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This Script is used for the non-playable characters in the game
/// this allows information about the npcs to be stored and accessed.
/// The code used in this script are for the following in this summary
/// -----------------------------------------
/// damage to the npc including death function when npc Health is zero.
/// weapon spawn and putting them in the correct slots on the npc.
/// changing weapon position to correct slots when different animations parameters are true.
/// Npc sensors to detect the player position.
/// Npc colliders for the weapons they will use. 
/// </summary>

public class NpcEnemy : MonoBehaviour
{
    Animator animator;
    private gameManager gamemanager;
    public LayerMask Player;
    //public GameQuest quest;
    private QuestHandler questHandler;
    private SoundEffects soundEffects;

    [Header("Npc Character Weapons")]
    private GameObject NpcItemWeapon;
    private GameObject NpcItemWeapon2;
    private GameObject NpcItemWeapon3;
    [Header("Npc Character Slots")]
    public GameObject PointHandRight;
    public GameObject PointHandRight2;
    public GameObject PointHandRight3;
    public GameObject PointHandLeft;
    public GameObject PointHandLeft2;
    [Header("Npc Holster Slot")]
    public GameObject BackSlot;
    public GameObject BackSlotLeft;
    public GameObject BackSlotRight;
    public GameObject HipSlotRight;
    public GameObject HipSlotLeft;

    //npc boxColliders
    BoxCollider NpcWeaponBoxCollider;
    BoxCollider NpcWeaponBoxCollider2;
    BoxCollider NpcWeaponBoxCollider3;

    [SerializeField] ParticleSystem particleBlood;
    [SerializeField] ParticleSystem particleBlood2;

    [Header("Npc Stats")]
    public int damage = 15;
    public float MovementSpeed = 0;
    public float NpcMaxHp = 100;
    public float NpcHp;

    [Header("Npc Sensors")]
    public NavMeshAgent NpcMesh;
    public float NpcSight;
    public float NpcAttack;
    public float NpcWalkRange;
    public Transform playerPos;

    public bool PlayerInRange, AttackPlayer;

    private bool NpcAttacking;

    public int AttackState;
    public int AttackNumber;

    // Start is called before the first frame update
    private void Awake()
    {
        soundEffects = GetComponent<SoundEffects>();
        animator = GetComponent<Animator>();
        questHandler = FindObjectOfType<QuestHandler>();
        gamemanager = FindObjectOfType<gameManager>();
        NpcHp = NpcMaxHp;
        Debug.Log(NpcHp);
        NpcSpawnWeapon();
        

    }

    void Update()
    {
        PlayerInRange = Physics.CheckSphere(transform.position, NpcSight, Player);
        AttackPlayer = Physics.CheckSphere(transform.position, NpcAttack, Player);


    }


    public void Damage(float dmg)
    {
        NpcHp -= dmg;
        Debug.Log("Npc Health at " + NpcHp);
        particleBlood.Play();
        soundEffects.HitSound(); 
        if (NpcHp <= 0)
        {
            particleBlood2.Play();
            //when the npc hp is 0 or less it will play npc death function 
            NpcDeath();
            
            //npc health == <0 set goal Npc kill count to -1
            
        }

    }

    public void NpcDeath()
    {

        //Destroy(gameObject);
        //disabling these componets will stop the npc from moving  
        //as well stop it being affects by colliders.
        //this will let unity game physic take over and create a ragroll to the Npc.
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Animator>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<AiStateMachine>().enabled = false;
        GetComponent<NpcMovement>().enabled = false;
        questHandler.NpcKilled();

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, NpcWalkRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, NpcSight);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, NpcAttack);
    }

    /// <summary>
    /// For the Npc weapon Spawn and holstering and the collison works the same as the Players code 
    /// though this code has been chnaged to work for the npc only allowing the npc to trigger weapon functions 
    /// at the correct points in animations and without the use of Input system which the player functions are needed.
    /// </summary>
    void NpcSpawnWeapon()//Spawn weapon objects
    {
        NpcItemWeapon = Instantiate(gamemanager.NpcMeleeWeapons.GetComponent<Weapons>().weaponsArray[2]);//DoubleAxe
        NpcItemWeapon2 = Instantiate(gamemanager.NpcMeleeWeapons.GetComponent<Weapons>().weaponsArray[1]);//Shield
        NpcItemWeapon3 = Instantiate(gamemanager.NpcMeleeWeapons.GetComponent<Weapons>().weaponsArray[0]);//Sword

        NpcWeaponBoxCollider = NpcItemWeapon.GetComponent<BoxCollider>();
        NpcWeaponBoxCollider2 = NpcItemWeapon2.GetComponent<BoxCollider>();
        NpcWeaponBoxCollider3 = NpcItemWeapon3.GetComponent<BoxCollider>();

        NpcWeaponBoxCollider.enabled = false;
        NpcWeaponBoxCollider2.enabled = false;
        NpcWeaponBoxCollider3.enabled = false;


        NpcWeaponHolster();//This function is called to put the spawn weapons into the correct positions in the game

    }

    public void NpcWeaponHolster() {

        if (AttackState == 0)
        {
            //double Axe
            NpcItemWeapon.transform.parent = HipSlotLeft.transform;
            NpcItemWeapon.transform.localPosition = Vector3.zero;
            NpcItemWeapon.transform.localEulerAngles = Vector3.zero;

            //Shield
            NpcItemWeapon2.transform.parent = BackSlot.transform;
            NpcItemWeapon2.transform.localPosition = Vector3.zero;
            NpcItemWeapon2.transform.localEulerAngles = Vector3.zero;

            //Sword
            NpcItemWeapon3.transform.parent = HipSlotRight.transform;
            NpcItemWeapon3.transform.localPosition = Vector3.zero;
            NpcItemWeapon3.transform.localEulerAngles = Vector3.zero;

        }
        else if (AttackState == 1)
        {
            //double Axe
            NpcItemWeapon.transform.parent = PointHandRight.transform;
            NpcItemWeapon.transform.localPosition = Vector3.zero;
            NpcItemWeapon.transform.localEulerAngles = Vector3.zero;

            //Shield
            NpcItemWeapon2.transform.parent = BackSlot.transform;
            NpcItemWeapon2.transform.localPosition = Vector3.zero;
            NpcItemWeapon2.transform.localEulerAngles = Vector3.zero;

            //Sword
            NpcItemWeapon3.transform.parent = HipSlotRight.transform;
            NpcItemWeapon3.transform.localPosition = Vector3.zero;
            NpcItemWeapon3.transform.localEulerAngles = Vector3.zero;
            //postions of the weapons for each attackstate
        }
        else if (AttackState == 2) {

            //double Axe
            NpcItemWeapon.transform.parent = HipSlotLeft.transform;
            NpcItemWeapon.transform.localPosition = Vector3.zero;
            NpcItemWeapon.transform.localEulerAngles = Vector3.zero;

            //Shield
            NpcItemWeapon2.transform.parent = PointHandLeft2.transform;
            NpcItemWeapon2.transform.localPosition = Vector3.zero;
            NpcItemWeapon2.transform.localEulerAngles = Vector3.zero;

            //Sword
            NpcItemWeapon3.transform.parent = PointHandRight3.transform;
            NpcItemWeapon3.transform.localPosition = Vector3.zero;
            NpcItemWeapon3.transform.localEulerAngles = Vector3.zero;
        }
       
    }

    //like the player collision but setup for the Npc Characters.
    public void NpcCollisionStart()
    {
    
        if (AttackState == 1)
        {
            NpcWeaponBoxCollider.enabled = true;// box collider for axe

           
        }
        if (AttackState == 2)
        {
            //box collider for sword and shield.
            NpcWeaponBoxCollider2.enabled = true;
            NpcWeaponBoxCollider3.enabled = true;

        }
       
    }
    public void NpcCollisionEnd()
    {
        NpcWeaponBoxCollider.enabled = false;

        NpcWeaponBoxCollider2.enabled = false;

        NpcWeaponBoxCollider3.enabled = false;

    }



}
