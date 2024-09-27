using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStoppingState : PlayerGroundedState
{
    public PlayerStoppingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (playerStateMachine.ReusableData.MovementInput == Vector2.zero || playerStateMachine.ReusableData.MovementSpeedModifier == 0)
        {
            return;
        }

        StartAnimation(playerStateMachine.Player.AnimationData.StoppingParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(playerStateMachine.Player.AnimationData.StoppingParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base .PhysicsUpdate();

        if (!IsMovingHorizontally())
        {
            return;
        }

        DecelerateHorizontally();
    }

    public override void OnAnimationTransitionEvent()
    {
        playerStateMachine.ChangeState(playerStateMachine.MovementStateMachine.IdlingState);
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        playerStateMachine.Player.Input.PlayerActions.Movement.started += OnMovementStarted;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        playerStateMachine.Player.Input.PlayerActions.Movement.started -= OnMovementStarted;
    }

    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        OnMove();
    }
}
