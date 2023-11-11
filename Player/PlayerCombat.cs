using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
   
    AnimatorManager animatorManager;
    InputManager inputManager;
  
    public int ComboNumber =0;
    public string preivousAttack;


    private void Awake()
    {
       animatorManager = GetComponentInChildren<AnimatorManager>();
       inputManager = GetComponent<InputManager>();
       

 /*   }
    public void LightAttack(ItemWeapon weapon)
    {
        animatorManager.PlayAnimation(weapon.lightAttack, true);
        preivousAttack = weapon.lightAttack;
    }

    public void HeavyAttack(ItemWeapon weapon)
    {

        animatorManager.PlayAnimation(weapon.heavyAttack, true);
        preivousAttack = weapon.heavyAttack;

    }

    public void BlockAttack(ItemWeapon weapon)
    {

       animatorManager.PlayAnimation(weapon.Block,true);
     

    }


     public void AttackCombo(ItemWeapon weapon) {

        //when the player clicks attack button and then press again attack before and then play next attack
        //if player attack is click within a setup time play combo else if player doesn't click within set up set time to 0 and combo to false.

        if (inputManager.comboCheck)
        {
            animatorManager.animator.SetBool("Combo", false);
            Debug.Log("attack was" + preivousAttack);
            if (preivousAttack == weapon.lightAttack)
            {
                animatorManager.PlayAnimation(weapon.lightAttack2, true);
            }

        }
*/
      }
}
