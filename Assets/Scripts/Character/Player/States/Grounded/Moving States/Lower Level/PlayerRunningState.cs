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

        playerStateMachine.ReusableData.MovementSpeedModifier = movementData.RunData.SpeedModifier;

        StartAnimation(playerStateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(playerStateMachine.Player.AnimationData.RunParameterHash);
    }

    public override void Update()
    {
        base.Update();

        //if (stateMachine.Player.Input.PlayerActions.Attack.triggered)
        //{
        //    OnAttack();
        //}
        StopRunning();
    }

    private void StopRunning()
    {
        if (playerStateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            playerStateMachine.ChangeState(playerStateMachine.MovementStateMachine.IdlingState);

            return;
        }
    }

    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        playerStateMachine.ChangeState(playerStateMachine.MovementStateMachine.MediumStoppingState);

        base.OnMovementCanceled(context);
    }
}
