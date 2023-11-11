using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoolen : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //This plays after the landing state which will play the land animation.

    public string isPlayerBoolName;
    public bool isPlayerStatus;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //sets the bool to false.
        animator.SetBool(isPlayerBoolName, isPlayerStatus);

    }
}
