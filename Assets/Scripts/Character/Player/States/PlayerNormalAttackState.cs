using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackState : PlayerCombatState
{
    public PlayerNormalAttackState(PlayerMovementStateMachine playerMovementStateMachine, PlayerCombatStateMachine playerCombatStateMachine) : base(playerMovementStateMachine, playerCombatStateMachine)
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
