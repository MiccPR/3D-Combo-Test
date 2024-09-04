using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.PlayerActions PlayerActions { get; private set; }

    private Vector2 movementInput;

    private float speed = 5f;

    private Rigidbody rb;

    public Transform PlayerCamera { get; private set; }

    private Vector3 currentTargetRotation;
    private Vector3 timeToReachTargetRotation;
    private Vector3 dampedTargetRotationCurrentVelocity;
    private Vector3 dampedTargetRotationPassedTime;

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        PlayerActions = InputActions.Player;
        rb = GetComponent<Rigidbody>();
        PlayerCamera = Camera.main.transform;
        InitializeData();
    }

    private void InitializeData()
    {
        timeToReachTargetRotation.y = 0.14f;

    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInput()
    {
        movementInput = PlayerActions.Movement.ReadValue<Vector2>();
    }

    private void HandleMovement()
    {
        if (movementInput == Vector2.zero)
        {
            return;
        }

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);

        float targetRotationYAngle = Rotate(move);

        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

        Vector3 horizontalMovement = rb.velocity;
        horizontalMovement.y = 0;

        rb.AddForce(targetRotationDirection * speed - horizontalMovement, ForceMode.VelocityChange);
    }

    private Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    private float Rotate(Vector3 direction)
    {
        float directinAngle = UpdateTargetRotation(direction);

        RotateTowardsTargetRotation();

        return directinAngle;
    }

    private float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (shouldConsiderCameraRotation)
        {
            directionAngle = AddCameraRotation(directionAngle);
        }

        if (directionAngle != currentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }

    private void RotateTowardsTargetRotation()
    {
        float currentYAngle = rb.rotation.eulerAngles.y;

        if (currentYAngle == currentTargetRotation.y)
        {
            return;
        }

        float smoothYAngle = Mathf.SmoothDampAngle(currentYAngle, currentTargetRotation.y, ref dampedTargetRotationCurrentVelocity.y, timeToReachTargetRotation.y - dampedTargetRotationPassedTime.y);

        dampedTargetRotationPassedTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smoothYAngle, 0f);

        rb.MoveRotation(targetRotation);
    }

    private float AddCameraRotation(float angle)
    {
        angle += PlayerCamera.eulerAngles.y;

        if (angle > 360f)
        {
            angle -= 360f;
        }

        return angle;
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

    private void UpdateTargetRotationData(float targetAngle)
    {
        currentTargetRotation.y = targetAngle;

        dampedTargetRotationPassedTime.y = 0f;
    }
}
