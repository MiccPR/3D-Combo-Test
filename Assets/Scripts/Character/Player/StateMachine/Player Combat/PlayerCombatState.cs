using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatState : IState
{
    protected PlayerStateMachine playerStateMachine;

    protected PlayerCombatStateMachine combatStateMachine;

    // protected PlayerMovementStateMachine movementStateMachine;

    protected PlayerGroundedData movementData;

    public PlayerCombatState(PlayerCombatStateMachine playerCombatStateMachine)
    {
        playerStateMachine = playerCombatStateMachine.PlayerStateMachine;

        //movementStateMachine = playerCombatStateMachine.PlayerMovementStateMachine;
        //movementData = movementStateMachine.Player.Data.GroundedData;

        combatStateMachine = playerCombatStateMachine;

        // InitializeData();
    }

    //private void InitializeData()
    //{
    //    movementStateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
    //}

    #region IState Methods
    public virtual void Enter()
    {
        Debug.Log("State: " + GetType().Name);

        // AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        // RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        // ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        // HandleMovement();
    }

    public virtual void Update()
    {
        HandleInput();
    }

    public virtual void OnAnimationEnterEvent()
    {

    }

    public virtual void OnAnimationExitEvent()
    {

    }

    public virtual void OnAnimationTransitionEvent()
    {

    }
    #endregion

    //private void ReadMovementInput()
    //{
    //    movementStateMachine.ReusableData.MovementInput = movementStateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    //}

    protected void StartAnimation(int animationHash)
    {
        playerStateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        playerStateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected void ResetVelocity()
    {
        playerStateMachine.Player.RigidBody.velocity = Vector3.zero;
    }

    //protected virtual void OnMove()
    //{
    //    movementStateMachine.ChangeState(movementStateMachine.RunningState);
    //}

    //protected virtual void AddInputActionsCallbacks()
    //{
    //    movementStateMachine.Player.Input.PlayerActions.Movement.canceled += OnMoveCancled;
    //}

    //protected virtual void RemoveInputActionsCallbacks()
    //{
    //    movementStateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMoveCancled;
    //}

    //protected virtual void OnMoveCancled(InputAction.CallbackContext context)
    //{
    //    movementStateMachine.ChangeState(movementStateMachine.IdleState);
    //}
}
