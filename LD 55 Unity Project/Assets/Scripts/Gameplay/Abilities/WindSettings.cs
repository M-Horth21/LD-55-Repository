using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wind Settings")]
public class WindSettings : ScriptableObject
{
    [SerializeField] public LayerMask affectedLayers;
    [SerializeField] public float rechargeTime;
    [SerializeField] public float abilityTime;
    [SerializeField] public float windStrength;
    [SerializeField] public GameObject windPrefab;
}
