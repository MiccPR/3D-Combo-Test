using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGroundedState : PlayerMovementState
{
    public PlayerGroundedState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(stateMachine.Player.AnimationData.GroundedParameterHash);
    }

    public override void Exit()
    {
        base.Enter();

        StopAnimation(stateMachine.Player.AnimationData.GroundedParameterHash);
    }

    protected virtual void OnMove()
    {
        stateMachine.ChangeState(stateMachine.RunningState);
    }

    protected virtual void OnAttack()
    {
        // stateMachine.ChangeState(stateMachine.NormalAttackState);
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Movement.canceled += OnMoveCancled;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        stateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMoveCancled;
    }

    protected virtual void OnMoveCancled(InputAction.CallbackContext context)
    {
        stateMachine.ChangeState(stateMachine.IdleState);
    }
}
