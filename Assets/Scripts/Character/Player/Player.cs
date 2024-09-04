using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody RigidBody {  get; private set; }

    public Animator Animator { get; private set; }

    private PlayerStateMachine playerStateMachine;

    public PlayerInput Input {  get; private set; }

    public Transform PlayerCamera { get; private set; }

    private void Awake()
    {
        playerStateMachine = new PlayerStateMachine(this);
        RigidBody = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();
        PlayerCamera = Camera.main.transform;

        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        playerStateMachine.ChangeState(playerStateMachine.MovementStateMachine.IdleState);
    }

    private void Update()
    {
        playerStateMachine.Update();
    }

    private void FixedUpdate()
    {
        playerStateMachine.PhysicsUpdate();
    }
}
