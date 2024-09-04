using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraZoom : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 10f)] private float defaultDistance = 4f;
    [SerializeField]
    [Range(0f, 10f)] private float minDistance = 2f;
    [SerializeField]
    [Range(0f, 10f)] private float maxDistance = 10f;

    [SerializeField]
    [Range(0f, 10f)] private float smoothing = 4f;
    [SerializeField]
    [Range(0f, 10f)] private float zoomSensitivity = 1f;

    private CinemachineFramingTransposer framingTransposer;

    private CinemachineInputProvider inputProvider;

    private float currentTargetDistance;

    public PlayerInputActions InputActions { get; private set; }

    void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();

        inputProvider = GetComponent<CinemachineInputProvider>();

        currentTargetDistance = defaultDistance;

        InputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        InputActions.Enable();
        InputActions.Player.Zoom.performed += OnZoomPerformed;
    }

    private void OnDisable()
    {
        InputActions.Disable();
        InputActions.Player.Zoom.performed -= OnZoomPerformed;
    }

    private void OnZoomPerformed(InputAction.CallbackContext context)
    {
        Zoom(context);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    Zoom();
    //}

    private void Zoom(InputAction.CallbackContext context)
    {
        float zoomValue = context.ReadValue<float>() * zoomSensitivity;

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minDistance, maxDistance);

        float currentDistance = framingTransposer.m_CameraDistance;

        if (currentDistance == currentTargetDistance)
        {
            return;
        }

        // float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);

        framingTransposer.m_CameraDistance = currentTargetDistance;
    }
}
