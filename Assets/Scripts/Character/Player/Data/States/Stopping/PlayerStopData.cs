using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStopData
{
    [field: SerializeField][field: Range(0f, 15f)] public float MediumDecelerationForce { get; private set; } = 6.5f;
}
