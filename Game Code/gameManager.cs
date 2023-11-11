using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    public GameObject meleeWeapon;
    public GameObject NpcMeleeWeapons;
    public GameObject gameOverScreen;

   public void GameOverLost()
    {
        print("GameOver You Died");
        Time.timeScale = 0.0f;
        gameOverScreen.SetActive(true);
    }
   

  
}
