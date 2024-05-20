using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AIAnimation : MonoBehaviour
{
    //private AIAgent aiAgent;
    private Animator animator;
    private readonly int isLeft = Animator.StringToHash("isLeft");
    private readonly int isRight = Animator.StringToHash("isRight");

    //[SerializeField] private GameObject headAimTarget;

    private void Awake()
    {
        //aiAgent = GetComponent<AIAgent>();
        animator = GetComponent<Animator>();
    }

    public void SetHeadAim(GameObject ball)
    {

    }

    public void MoveAnim(float z)
    {
     /*if (z != 0)
        {
            if (z > 0)
            {
                animator.SetBool(isRight, false);
                animator.SetBool(isLeft, true);
            }
            else
            {
                animator.SetBool(isRight, true);
                animator.SetBool(isLeft, false);
            }
        }
        else
        {
            animator.SetBool(isRight, false);
            animator.SetBool(isLeft, false);
        }*/
    }
}
