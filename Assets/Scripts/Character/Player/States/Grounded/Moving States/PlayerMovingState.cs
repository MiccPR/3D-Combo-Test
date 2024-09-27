using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovingState : PlayerGroundedState
{
    public PlayerMovingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(playerStateMachine.Player.AnimationData.MovingParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(playerStateMachine.Player.AnimationData.MovingParameterHash);
    }
}
