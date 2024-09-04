using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMovingState
{
    public PlayerRunningState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        movementStateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;

        StartAnimation(movementStateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(movementStateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();

        //if (stateMachine.Player.Input.PlayerActions.Attack.triggered)
        //{
        //    OnAttack();
        //}
    }
}
