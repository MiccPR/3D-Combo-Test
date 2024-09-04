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

        StartAnimation(movementStateMachine.Player.AnimationData.GroundedParameterHash);
    }

    public override void Exit()
    {
        base.Enter();

        StopAnimation(movementStateMachine.Player.AnimationData.GroundedParameterHash);
    }

    protected virtual void OnMove()
    {
        playerStateMachine.ChangeState(movementStateMachine.RunningState);
    }

    protected virtual void OnAttack()
    {
        playerStateMachine.ChangeState(playerStateMachine.CombatStateMachine.NormalAttackState); // need to look after
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        movementStateMachine.Player.Input.PlayerActions.Movement.canceled += OnMoveCancled;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        movementStateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMoveCancled;
    }

    protected virtual void OnMoveCancled(InputAction.CallbackContext context)
    {
        playerStateMachine.ChangeState(movementStateMachine.IdleState);
    }
}
