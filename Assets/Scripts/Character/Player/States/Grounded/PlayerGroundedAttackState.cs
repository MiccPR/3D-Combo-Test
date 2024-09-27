using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedAttackState : PlayerCombatState
{
    public PlayerGroundedAttackState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StartAnimation(playerStateMachine.Player.AnimationData.GroundedParameterHash);
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(playerStateMachine.Player.AnimationData.GroundedParameterHash);
    }

    protected virtual void OnMove()
    {
        playerStateMachine.ChangeState(playerStateMachine.MovementStateMachine.RunningState);
    }

    protected virtual void OnAttack()
    {
        playerStateMachine.ChangeState(playerStateMachine.CombatStateMachine.NormalAttackState);
    }
}
