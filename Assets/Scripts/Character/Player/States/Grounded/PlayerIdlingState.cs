using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdlingState : PlayerGroundedState
{
    public PlayerIdlingState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerStateMachine.ReusableData.MovementSpeedModifier = 0;

        StartAnimation(playerStateMachine.Player.AnimationData.IdleParameterHash);

        ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(playerStateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (playerStateMachine.Player.Input.PlayerActions.Attack.triggered)
        {
            OnAttack();
        }

        if (playerStateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            return;
        }

        OnMove();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!IsMovingHorizontally())
        {
            return;
        }

        ResetVelocity();
    }
}
