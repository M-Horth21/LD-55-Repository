using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Punch Settings")]
public class PunchSettings : ScriptableObject
{
    [SerializeField] public float rechargeTime;
    [SerializeField] public GameObject punchPrefab;
}
