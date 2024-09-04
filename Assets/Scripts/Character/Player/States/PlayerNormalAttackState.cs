using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackState : PlayerCombatState
{
    public PlayerNormalAttackState(PlayerCombatStateMachine playerCombatStateMachine) : base(playerCombatStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        movementStateMachine.ReusableData.MovementSpeedModifier = 0;

        ResetVelocity();
    }

    public override void Update()
    {
        base.Update();

        
    }
}
