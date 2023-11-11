using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        
       PlayerManger playerStates = other.GetComponent<PlayerManger>();


        if (playerStates != null)
        {
            playerStates.PlayerDamage(damage);
        }
    }
}
