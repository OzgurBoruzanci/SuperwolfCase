using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField]private bool isCarrying = false;
    private bool isWalking = false;
    public bool IsWalking { get => isWalking; set => isWalking = value; }
    public bool IsCarrying { get => isCarrying; set => isCarrying = value; }

    public void AnimationControl()
    {
        if (isWalking && !isCarrying)
        {
            SetWalkingAnimatio();
        }
        else if (isWalking && isCarrying)
        {
            SetCarryingWalkAnimatio();
        }
        else if (!isWalking && isCarrying)
        {
            SetCarryingIdleAnimation();
        }
        else
        {
            SetIdleAnimation();
        }
    }
    private void SetIdleAnimation()
    {
        animator.SetBool("Walking", false);
        animator.SetBool("CarryingBoxWalk", false);
        animator.SetBool("CarryingIdle", false);
    }
    private void SetCarryingIdleAnimation()
    {
        animator.SetBool("CarryingIdle", true);
        animator.SetBool("CarryingBoxWalk", false);
    }
    private void SetWalkingAnimatio()
    {
        animator.SetBool("Walking", true);
    }
    private void SetCarryingWalkAnimatio()
    {
        animator.SetBool("CarryingBoxWalk", true);
    }
}
