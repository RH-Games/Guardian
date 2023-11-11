using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine.AI;
using UnityEngine;

public class AiStateMachine : MonoBehaviour
{
    private States currentState;
   
    private Vector3 PlayerPosition;//target player
    private Vector3 startingNpcPos;// start position of Npc
    private Vector3 roamNpcPos; //
    
    private Rigidbody body;
    private Animator animator;
    private NpcEnemy npcEnemyScript;

    public NavMeshAgent navAgent;
    public Transform player;
    public LayerMask Player, Ground;

    //roaming
    public Vector3 walkPoint;
    bool walkpointSet;
  
    //attack
    public float attackTimer;
    public bool attacking;
    public int RandomState;

    public enum States
    {
        //these are the different states the Npc can go to.
        Idle,  //the default state
               //Idle to roam is Unconditional
        roam,  //movement state
               //roam to chase is conditional and will only happen when npc sees player
        Chase, //chase after player withing a set range/distance
               //chase to attack is coniditonal and will happen when close to the player
        Attack //Npc Attacks Player
            
            //if the player moves out of attack rannge the Npc will chnage to chase state,
            //if player moves and of view the Npc will go back to roam or idle. 
    }

    private void Awake()
    {
        player = GameObject.Find("PlayerCharacter").transform;
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        npcEnemyScript = GetComponent<NpcEnemy>();


    }

    private void Update()
    {
        //checks if the player is in range of being chased or Attacked
        if (!npcEnemyScript.PlayerInRange && !npcEnemyScript.AttackPlayer)
        {
            
            currentState = States.roam;
          
        }
        if (npcEnemyScript.PlayerInRange && !npcEnemyScript.AttackPlayer)
        {
            currentState = States.Chase;
        }
        else if (npcEnemyScript.AttackPlayer && npcEnemyScript.PlayerInRange)
        {
            currentState = States.Attack;

        }
         StateCheck();//sets the correct state 
    }

    private void StateCheck() {
        //checks current state and changes it to the correct state.
        if (currentState == States.roam)
        {
            Roam();
        }
        else if (currentState == States.Chase)
        {
          
            Chase();
        }
        else if (currentState == States.Attack)
        {

            Attack();
        }
    }


    //sets a new walk point for the Npc to move to.
    private void SearchWalkPoint()
    {
        float walkRange = npcEnemyScript.NpcWalkRange; //gets the npcwalkrange from npc enemy 
        float randomZ= Random.Range(-walkRange, walkRange); //creates a random float for the Z axis 
        float randomX = Random.Range(-walkRange, walkRange); //creates a random float for the y axis 

        //makes the walk point from the two randoms axis and add them to the transform position.
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        //checks if the walkpoint is on ground and within map area.
        if (Physics.Raycast(walkPoint, -transform.up, 2F, Ground))
            walkpointSet = true;

    }
 

    private void Roam() //This state sets the npc to roam around random area on map
    {
   
        print("Roaming");
        //Roam Code here
        if (!walkpointSet) // there is no walk point it will run the search point which will make one
        {
            SearchWalkPoint();
        }
        if (walkpointSet) // if there is a walk point set the nav agent will set the destination to that se point.
        {
            navAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToPoint = transform.position - walkPoint; // gets the distance from the point

        if(distanceToPoint.magnitude < 2f) //when the Npc is at point less then 1f. it wil set walk point to false.
        {
            walkpointSet=false;
        }

        //walking animations
        animator.SetBool("isRoaming", true);
        animator.SetFloat("Vertical",1);
        animator.SetInteger("AttackState", 0);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isChasing", false);
        npcEnemyScript.AttackState = 0;
    }

    private void Chase()
    {
        print("Chase Player");

        navAgent.SetDestination(player.position);
        if (npcEnemyScript.AttackState == 0)
        {
            SetAttackState();
            
        }
        //chase animation
        animator.SetBool("isChasing", true);
        animator.SetFloat("Vertical", 1);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRoaming", false);
        //animator.SetInteger("AttackState", RandomState);
        //npcEnemyScript.AttackState = RandomState;
        

    }

    private void Attack()
    {
        
        navAgent.SetDestination(transform.position);

        transform.LookAt(player);
       
        if (!attacking)
        {
            print("attack");//if not attacking set attacking to true and play animation.
            animator.SetBool("isAttacking", true);
                                                 //random.range (is included , not included)
            animator.SetInteger("AttackNumber", Random.Range(1, 4)); // choose random attack animation from 1,2 or 3 attack number.
            attacking = true;
            Invoke(nameof(ResetAttacking), attackTimer);//invoke schedules a method to occur at a set time attacktimer
        }
        //AtTarget

        //attack animations 
        animator.SetFloat("Vertical", 0);
        //animator.SetInteger("AttackState", 2);
        animator.SetBool("isChasing", false);
        animator.SetBool("isRoaming", false);
       


        //set attack bool true to play the attack animation 
    }

    private void ResetAttacking()
    {
        //resets values to false. 
        attacking=false;
        animator.SetBool("isAttacking", false);
        //animator.SetInteger("Attacknumber", 0);
        print("Reset");
    }

    private void SetAttackState() {
 
       int randomState = Random.Range(1, 3);

       animator.SetInteger("AttackState", randomState);
       npcEnemyScript.AttackState = randomState;
       npcEnemyScript.NpcWeaponHolster();

    }

}
