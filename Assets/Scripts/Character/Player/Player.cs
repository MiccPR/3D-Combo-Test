using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO Data { get; private set; }
    [field: SerializeField] public CharacterSO Character { get; private set; }

    public AnimatorOverrideController AnimatorOverrider { get; private set; }

    public Dictionary<string, AnimationClip> animationOverrides;

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

        AnimatorOverrider = Character.CharacterDemoAttackAnimatorOverrider;
        List<KeyValuePair<AnimationClip, AnimationClip>> overridesList = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        AnimatorOverrider.GetOverrides(overridesList);

        animationOverrides = new Dictionary<string, AnimationClip>();

        foreach (var pair in overridesList)
        {
            animationOverrides[pair.Key.name] = pair.Value;
        }
    }

    private void Start()
    {
        playerStateMachine.ChangeState(playerStateMachine.MovementStateMachine.IdlingState);
    }

    private void Update()
    {
        playerStateMachine.Update();
    }

    private void FixedUpdate()
    {
        playerStateMachine.PhysicsUpdate();
    }

    public void OnMovementStateAnimationEnterEvent()
    {
        playerStateMachine.OnAnimationEnterEvent();
    }

    public void OnMovementStateAnimationExitEvent()
    {
        playerStateMachine.OnAnimationExitEvent();
    }

    public void OnMovementStateAnimationTransitionEvent()
    {
        playerStateMachine.OnAnimationTransitionEvent();
    }
}
