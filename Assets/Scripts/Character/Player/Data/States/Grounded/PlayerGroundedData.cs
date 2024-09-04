using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerGroundedData
{

    [field: SerializeField] [Range(0f, 25f)] public float BaseSpeed { get; private set; } = 5f;

    [field: SerializeField] public PlayerRotationData BaseRotationData { get; private set; }

    [field: SerializeField] public PlayerRunData RunData { get; private set; }


}
