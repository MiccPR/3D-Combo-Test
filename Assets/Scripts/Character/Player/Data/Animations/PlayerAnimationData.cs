using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    [Header("State Group Parameter Names")]
    [SerializeField] private string groundedParameterName = "Grounded";
    [SerializeField] private string movingParameterName = "Moving";
    [SerializeField] private string attackingParameterName = "Attacking";
    [SerializeField] private string StoppingParameterName = "Stopping";

    [Header("Grounded Parameter Names")]
    [SerializeField] private string idleParameterName = "isIdling";
    [SerializeField] private string runParameterName = "isRunning";
    [SerializeField] private string attackParameterName = "isAttacking";
    // [SerializeField] private string StopParameterName = "isStopping";

    public int GroundedParameterHash {  get; private set; }
    public int MovingParameterHash { get; private set; }
    public int AttackingParameterHash { get; private set; }
    public int StoppingParameterHash { get; private set; }

    public int IdleParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    // public int StopParameterHash { get; private set; }

    public void Initialize()
    {
        GroundedParameterHash = Animator.StringToHash(groundedParameterName);
        MovingParameterHash = Animator.StringToHash(movingParameterName);
        AttackingParameterHash = Animator.StringToHash(attackingParameterName);
        StoppingParameterHash = Animator.StringToHash(StoppingParameterName);

        IdleParameterHash = Animator.StringToHash(idleParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        // StopParameterHash = Animator.StringToHash(StopParameterName);
    }
}
