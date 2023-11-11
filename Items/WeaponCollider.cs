using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    Collider dmgCollider;
    GameObject NpcE;
    NpcEnemy Npc;


    public float dmg;


    private void Awake()
    {
        NpcE = GameObject.FindGameObjectWithTag("NPC");

        dmgCollider = GetComponent<Collider>();
        dmgCollider.gameObject.SetActive(true);
        dmgCollider.isTrigger = true;
        dmgCollider.enabled = false;


    }

    public void EnableDmgCollider()
    {
        dmgCollider.enabled=true;
    }


    public void DisenableDmgCollider()
    {
        dmgCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag =="NPC" )
        {

            NpcEnemy Npc = collision.GetComponent<NpcEnemy>();
            if (Npc != null)
            {
                //Debug.Log("Did Hit");
                Npc.Damage(dmg);
            }

            //NpcE.GetComponent<NpcEnemy>().Damage(dmg);

        }
       /* if(collision.tag == "Player")
        {
            Debug.Log("Player Got Hit by Npc");
            
        }*/
    }

}
