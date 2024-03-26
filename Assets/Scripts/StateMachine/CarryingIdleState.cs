using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryingIdleState : IState
{
    public void EnterState(PlayerController player)
    {
        player.animator.SetBool("CarryingIdle", true);
        player.animator.SetBool("CarryingBoxWalk", false);
    }

    public void UpdateState(PlayerController player)
    {
        if (player.IsWalking && player.IsCarrying)
        {
            player.ChangeState(new TransportWalkingState());
        }
        else if (!player.IsWalking && !player.IsCarrying)
        {
            player.ChangeState(new IdleState());
        }
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Exiting Idle State");
    }
}
