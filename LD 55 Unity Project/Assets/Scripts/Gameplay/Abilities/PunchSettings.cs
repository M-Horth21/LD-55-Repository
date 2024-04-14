using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Punch Settings")]
public class PunchSettings : ScriptableObject
{
    [SerializeField] public LayerMask affectedLayers;
    [SerializeField] public float punchStrength;
    [SerializeField] public float rechargeTime;
    [SerializeField] public float abilityTime;
    [SerializeField] public GameObject punchPrefab;
    [SerializeField] public string punchWhif;
    [SerializeField] public string punchHit;
}
