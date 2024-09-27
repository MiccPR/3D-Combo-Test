using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Custom/Characters/Character")]
public class CharacterSO : ScriptableObject
{
    // - contains specific character information such as: damage, attacks, skills, ...

    // TODO for attack sequence animation: 
    // - create and see how many attack sequences this character has
    //      - idea:
    //          - using animator override controller to override the attacking animation state

    [field: SerializeField] public AnimatorOverrideController CharacterDemoAttackAnimatorOverrider { get; set; }
    [field: SerializeField] public int NumberOfNormalAttacks {  get; set; }

    [field: SerializeField] public AnimationClip[] Attacks { get; set; }
}
