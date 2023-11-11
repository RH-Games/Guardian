using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class QuestGoal 
{
   
    public GoalType goal;

    public int requiredKills;
    public int currentKills;

   
    public bool goalDone()
    {
        //if the current kills is the requiredKills or more return true.
        return (currentKills >= requiredKills);
    }

}

public enum GoalType
{
    kill
}
