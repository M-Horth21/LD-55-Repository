using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stun Settings")]
public class StunSettings : ScriptableObject
{
    [SerializeField] public LayerMask affectedLayers;
    [SerializeField, Range(0, 1)] public float stunStrength;
    [SerializeField] public float rechargeTime;
    [SerializeField] public float abilityTime;
    [SerializeField] public GameObject stunPrefab;
}
