using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : IState
{
    protected PlayerStateMachine playerStateMachine;

    // protected PlayerMovementStateMachine playerStateMachine;

    protected PlayerGroundedData movementData;

    public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
    {
        playerStateMachine = playerMovementStateMachine.PlayerStateMachine;

        // movementStateMachine = playerMovementStateMachine;
        movementData = playerStateMachine.Player.Data.GroundedData;

        InitializeData();
    }

    private void InitializeData()
    {
        playerStateMachine.ReusableData.TimeToReachTargetRotation = movementData.BaseRotationData.TargetRotationReachTime;
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

    #region Main Methods
    private void ReadMovementInput()
    {
        playerStateMachine.ReusableData.MovementInput = playerStateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        if (playerStateMachine.ReusableData.MovementInput == Vector2.zero || playerStateMachine.ReusableData.MovementSpeedModifier == 0f)
        {
            return;
        }

        Vector3 move = new Vector3(playerStateMachine.ReusableData.MovementInput.x, 0, playerStateMachine.ReusableData.MovementInput.y);

        float targetRotationYAngle = Rotate(move);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        float movementSpeed = GetMovementSpeed();

        Vector3 horizontalMovement = GetHorizontalMovement();

        playerStateMachine.Player.RigidBody.AddForce(targetRotationDirection * movementSpeed - horizontalMovement, ForceMode.VelocityChange);
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
        angle += playerStateMachine.Player.PlayerCamera.eulerAngles.y;

        if (angle > 360f)
        {
            angle -= 360f;
        }

        return angle;
    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        playerStateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;

        playerStateMachine.ReusableData.DampedTargetRotationPassedTime.y = 0f;
    }
    #endregion

    #region Reusable Methods
    protected void StartAnimation(int animationHash)
    {
        playerStateMachine.Player.Animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        playerStateMachine.Player.Animator.SetBool(animationHash, false);
    }

    protected virtual void AddInputActionsCallbacks()
    {
        playerStateMachine.Player.Input.PlayerActions.Movement.performed += OnMovementPerformed;
        playerStateMachine.Player.Input.PlayerActions.Movement.canceled += OnMovementCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        playerStateMachine.Player.Input.PlayerActions.Movement.performed -= OnMovementPerformed;
        playerStateMachine.Player.Input.PlayerActions.Movement.canceled -= OnMovementCanceled;
    }

    protected virtual void OnMovementPerformed(InputAction.CallbackContext context)
    {
        // UpdateCameraRecenteringState(context.ReadValue<Vector2>());
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        // DisableCameraRecentering();
    }

    protected Vector3 GetHorizontalMovement()
    {
        Vector3 horizontalMovement = playerStateMachine.Player.RigidBody.velocity;
        horizontalMovement.y = 0;
        return horizontalMovement;
    }

    protected float GetMovementSpeed()
    {
        return movementData.BaseSpeed * playerStateMachine.ReusableData.MovementSpeedModifier;
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

        if (directionAngle != playerStateMachine.ReusableData.CurrentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }

    protected void RotateTowardsTargetRotation()
    {
        float currentYAngle = playerStateMachine.Player.RigidBody.rotation.eulerAngles.y;

        if (currentYAngle == playerStateMachine.ReusableData.CurrentTargetRotation.y)
        {
            return;
        }

        float smoothYAngle = Mathf.SmoothDampAngle(currentYAngle, playerStateMachine.ReusableData.CurrentTargetRotation.y, ref playerStateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, playerStateMachine.ReusableData.TimeToReachTargetRotation.y - playerStateMachine.ReusableData.DampedTargetRotationPassedTime.y);

        playerStateMachine.ReusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smoothYAngle, 0f);

        playerStateMachine.Player.RigidBody.MoveRotation(targetRotation);
    }

    protected void ResetVelocity()
    {
        playerStateMachine.Player.RigidBody.velocity = Vector3.zero;
    }

    protected void DecelerateHorizontally()
    {
        Vector3 playerHorizontalVelocity = GetHorizontalMovement();

        playerStateMachine.Player.RigidBody.AddForce(-playerHorizontalVelocity * playerStateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
    }

    protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
    {
        Vector3 playerHorizontalVelocity = GetHorizontalMovement();

        Vector2 playerHorizontalMovemnt = new Vector2(playerHorizontalVelocity.x, playerHorizontalVelocity.z);

        return playerHorizontalMovemnt.magnitude > minimumMagnitude;
    }
    #endregion
}
