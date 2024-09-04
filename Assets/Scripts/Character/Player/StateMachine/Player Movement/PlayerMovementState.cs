using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerStateMachine playerStateMachine;

    protected PlayerMovementStateMachine movementStateMachine;

    protected PlayerCombatStateMachine combatStateMachine;

    protected PlayerGroundedData movementData;

    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        playerStateMachine = playerMovementStateMachine.PlayerStateMachine;

        movementStateMachine = playerMovementStateMachine;
        movementData = movementStateMachine.Player.Data.GroundedData;

        combatStateMachine = playerMovementStateMachine.CombatStateMachine;

        InitializeData();
    }

    private void InitializeData()
    {
        movementStateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
    }

    #region IState Methods
    public virtual void Enter()
    {
        Debug.Log("State: " + GetType().Name);

        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void PhysicsUpdate()
    {
        HandleMovement();
    }

    public virtual void Update()
    {
        HandleInput();
    }

    public virtual void OnAnimationEnterEvents()
    {
        
    }

    public virtual void OnAnimationExitEvents()
    {
        
    }

    public virtual void OnAnimationTransitionEvents()
    {
        
    }
    #endregion

    #region Main Methods
    private void ReadMovementInput()
    {
        movementStateMachine.ReusableData.MovementInput = movementStateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        if (movementStateMachine.ReusableData.MovementInput == Vector2.zero || movementStateMachine.ReusableData.MovementSpeedModifier == 0f)
        {
            return;
        }

        Vector3 move = new Vector3(movementStateMachine.ReusableData.MovementInput.x, 0, movementStateMachine.ReusableData.MovementInput.y);

        float targetRotationYAngle = Rotate(move);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMovementSpeed();

        Vector3 horizontalMovement = GetHorizontalMovement();

        movementStateMachine.Player.RigidBody.AddForce(targetRotationDirection * movementSpeed - horizontalMovement, ForceMode.VelocityChange);
    }
    private float Rotate(Vector3 direction)
    {
        float directinAngle = UpdateTargetRotation(direction);

        RotateTowardsTargetRotation();

        return directinAngle;
    }

    private float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0f)
        {
            directionAngle += 360f;
        }

        return directionAngle;
    }

    private float AddCameraRotationAngle(float angle)
    {
        angle += movementStateMachine.Player.PlayerCamera.eulerAngles.y;

        if (angle > 360f)
        {
            angle -= 360f;
        }

        return angle;
    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        movementStateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;

        movementStateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
    }
    #endregion

    #region Reusable Methods
    protected void StartAnimation(int animationHash)
    {
        movementStateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        movementStateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected virtual void AddInputActionsCallbacks()
    {

    }

    protected virtual void RemoveInputActionsCallbacks()
    {

    }

    protected Vector3 GetHorizontalMovement()
    {
        Vector3 horizontalMovement = movementStateMachine.Player.RigidBody.velocity;
        horizontalMovement.y = 0;
        return horizontalMovement;
    }

    protected float GetMovementSpeed()
    {
        return movementData.BaseSpeed * movementStateMachine.ReusableData.MovementSpeedModifier;
    }

    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (shouldConsiderCameraRotation)
        {
            directionAngle = AddCameraRotationAngle(directionAngle);
        }

        if (directionAngle != movementStateMachine.ReusableData.CurrentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }

    protected void RotateTowardsTargetRotation()
    {
        float currentYAngle = movementStateMachine.Player.RigidBody.rotation.eulerAngles.y;

        if (currentYAngle == movementStateMachine.ReusableData.CurrentTargetRotation.y)
        {
            return;
        }

        float smoothYAngle = Mathf.SmoothDampAngle(currentYAngle, movementStateMachine.ReusableData.CurrentTargetRotation.y, ref movementStateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, movementStateMachine.ReusableData.TimeToReachTargetRotation.y - movementStateMachine.ReusableData.DampedTargetRotationPassedTime.y);

        movementStateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smoothYAngle, 0f);

        movementStateMachine.Player.RigidBody.MoveRotation(targetRotation);
    }

    protected void ResetVelocity()
    {
        movementStateMachine.Player.RigidBody.velocity = Vector3.zero;
    }
    #endregion
}
