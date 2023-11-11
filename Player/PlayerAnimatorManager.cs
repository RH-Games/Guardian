using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public Animator animator;
    int horizonal;
    int vertical;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizonal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateAnimtor(float horizonalMove, float VerticalMove, bool playerRunning)
    {

        //Animation snapping helps the animations snap together better.
        float snapHorizontal;
        float snapVertical;
        #region 
        if (horizonalMove > 0 && horizonalMove < 0.55f)
        {
            snapHorizontal = 0.5f;
        }
        else if (horizonalMove > 0.55f)
        {
            snapHorizontal = 1.0f;
        }
        else if (horizonalMove < 0 && horizonalMove > -0.55f)
        {
            snapHorizontal = -0.5f;

        }
        else if (horizonalMove < -0.55f)
        {
            snapHorizontal = -1;
        }
        else
        {
            snapHorizontal = 0;
        }


        if (VerticalMove > 0 && VerticalMove < 0.55f)
        {
            snapVertical = 0.5f;
        }
        else if (VerticalMove > 0.55f)
        {
            snapVertical = 1.0f;
        }
        else if (VerticalMove < 0 && VerticalMove > -0.55f)
        {
            snapVertical = -0.5f;

        }
        else if (VerticalMove < -0.55f)
        {
            snapVertical = -1;
        }
        else
        {
            snapVertical = 0;
        }
        #endregion


        //sets the vertical to 2 if the player is running alone the vertical
        if (playerRunning)
        {
            snapHorizontal = horizonalMove;
            snapVertical = 2;
        }

        //sets the animator floats depending on the horizonalMove and VerticalMove.
        animator.SetFloat(horizonal, snapHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snapVertical, 0.1f, Time.deltaTime);
    }

    public void PlayAnimation(string Animation, bool PlayingAnimation)
    {
        animator.SetBool("isPlaying", PlayingAnimation);
        //from the current state to any other
        animator.CrossFade(Animation, 0.2f);
    }


}
