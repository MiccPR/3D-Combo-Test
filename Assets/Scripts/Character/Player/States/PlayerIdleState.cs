using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerMovementStateMachine playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        movementStateMachine.ReusableData.MovementSpeedModifier = 0;

        StartAnimation(movementStateMachine.Player.AnimationData.IdleParameterHash);

        ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(movementStateMachine.Player.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (movementStateMachine.Player.Input.PlayerActions.Attack.triggered)
        {
            OnAttack();
        }

        if (movementStateMachine.ReusableData.MovementInput == Vector2.zero)
        {
            return;
        }

        OnMove();
    }
}
