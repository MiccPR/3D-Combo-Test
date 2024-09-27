using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackState : PlayerGroundedAttackingState
{
    public PlayerNormalAttackState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        playerStateMachine.ReusableData.MovementSpeedModifier = 0;

        if (playerStateMachine.Player.animationOverrides.ContainsKey("Attack1"))
        {
            Debug.Log("Attack animation found in the AnimatorOverrideController!");

            playerStateMachine.Player.AnimatorOverrider["Attack1"] = playerStateMachine.Player.Character.Attacks[0];

            playerStateMachine.Player.Animator.runtimeAnimatorController = playerStateMachine.Player.AnimatorOverrider;
        }
        else
        {
            Debug.LogError("Attack animation not found in the AnimatorOverrideController!");
        }

        StartAnimation(playerStateMachine.Player.AnimationData.AttackParameterHash);

        ResetVelocity();
    }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(playerStateMachine.Player.AnimationData.AttackParameterHash);
    }

    public override void Update()
    {
        base.Update();

        
    }
}
