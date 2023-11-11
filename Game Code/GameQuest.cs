using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used to setup goal objective for the game.
[System.Serializable]
public class GameQuest
{
    public bool ActiveQuest;

    public string title;
    public string description;
    public int enemyCount;
    public int baseCount;

    public bool Completed;

    public string Won;

}
