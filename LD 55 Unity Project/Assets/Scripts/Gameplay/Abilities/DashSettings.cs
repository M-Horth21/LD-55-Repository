using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dash Settings")]
public class DashSettings : ScriptableObject
{
    [SerializeField] public float rechargeTime;
    [SerializeField] public float abilityTime;
    [SerializeField] public float dashForce;
    [SerializeField] public GameObject dashPrefab;
}
