using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : IState
{
    public void EnterState(PlayerController player)
    {
        player.animator.SetBool("Walking", true);
    }

    public void UpdateState(PlayerController player)
    {
        if (!player.IsCarrying && !player.IsWalking)
        {
            player.ChangeState(new IdleState());
        }
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Exiting Idle State");
    }
}
