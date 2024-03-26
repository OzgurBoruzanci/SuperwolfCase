using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportWalkingState : IState
{
    public void EnterState(PlayerController player)
    {
        player.animator.SetBool("CarryingBoxWalk", true);
    }

    public void UpdateState(PlayerController player)
    {
        if (player.IsCarrying && !player.IsWalking)
        {
            player.ChangeState(new CarryingIdleState());
        }
    }

    public void ExitState(PlayerController player)
    {
        Debug.Log("Exiting Idle State");
    }
}
