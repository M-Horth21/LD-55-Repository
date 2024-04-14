using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Punch Settings")]
public class PunchSettings : ScriptableObject
{
    [SerializeField] public LayerMask affectedLayers;
    [SerializeField] public float punchStrength;
    [SerializeField] public float rechargeTime;
    [SerializeField] public float abilityTime;
    [SerializeField] public GameObject punchPrefab;
}
