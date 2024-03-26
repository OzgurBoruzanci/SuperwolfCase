using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    public void EnterState(PlayerController player)
    {
        player.animator.SetBool("Walking", false);
        player.animator.SetBool("CarryingBoxWalk", false);
        player.animator.SetBool("CarryingIdle", false);
    }

    public void UpdateState(PlayerController player)
    {
        if (player.IsWalking && !player.IsCarrying)
        {
            player.ChangeState(new WalkingState());
        }
        else if (!player.IsWalking && player.IsCarrying)
        {
            player.ChangeState(new CarryingIdleState());
        }
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Exiting Idle State");
    }
}
