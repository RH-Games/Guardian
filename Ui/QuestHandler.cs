using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour
{
    public GameQuest quest;
    //public NpcEnemy npc;
    // public PlayerManger player;
    [Header("Game Over Screen")]
    public GameObject gameWonScreen;

    
    [Header("Game Goal Tab Settings")]
    public GameObject GoalWindow;
    public Text TitleTxt;
    public Text DescriptionTxt;
    public Text ObjectiveTxt;
    public Text ObjectiveTxt2;
    public Text won;
    
    public int requiredKills;
    public int currentKills;


    //this setups the game goals to be shown in the goal window that the user can view when its active.

    public void OpenGameGoals()
    {
      
        GoalWindow.SetActive(true);

        //setsup the gameGoal window

        TitleTxt.text = quest.title;
        DescriptionTxt.text = quest.description;
        ObjectiveTxt.text = "Bandits Killed: "+ currentKills.ToString();
        ObjectiveTxt2.text = "Bandits in area: " + requiredKills.ToString();
      

    }

    //when the npc is killed this will update the misson log.
    public void NpcKilled()
    {
        Debug.Log("Npc has been killed");

        currentKills ++;
        requiredKills--;
        ObjectiveTxt.text = "Bandits Killed: " + currentKills.ToString();
        ObjectiveTxt2.text = "Bandits in area: " + requiredKills.ToString();
        if (currentKills == 12)
        {
            quest.Completed = true;
            won.text = "The Land is safe Will Done";
            GameOverWin();

        }
    }

    
    public void GameOverWin()
    {

        gameWonScreen.SetActive(true);


    }
    
}
