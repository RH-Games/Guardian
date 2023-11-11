using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcWeaponCollider : MonoBehaviour
{

    Collider dmgCollider;
    GameObject player;
    PlayerManger playermanger;

    public int dmg;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
      
        dmgCollider = GetComponent<Collider>();
        dmgCollider.gameObject.SetActive(true);
        dmgCollider.isTrigger = true;
        dmgCollider.enabled = false;
    }


    public void EnableDmgCollider()
    {
        dmgCollider.enabled = true;
    }


    public void DisenableDmgCollider()
    {
        dmgCollider.enabled = false;
    }



    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Player")
        {

            PlayerManger player  = collision.GetComponent<PlayerManger>();
            if (player != null)
            {
                Debug.Log("Player Got Hit by Npc");
                player.PlayerDamage(dmg);

            }

        }

    }
}
