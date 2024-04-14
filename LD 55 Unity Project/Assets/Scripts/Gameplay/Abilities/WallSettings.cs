using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wall Settings")]
public class WallSettings : ScriptableObject
{
    [SerializeField] public float rechargeTime;
    [SerializeField] public float abilityTime;
    [SerializeField] public float wallDuration;
    [SerializeField] public GameObject wallPrefab;
    [SerializeField] public LayerMask wallLayer;
}
