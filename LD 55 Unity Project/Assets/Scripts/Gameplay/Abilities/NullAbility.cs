using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullAbility : IAbility
{
    public float recharge { get; }
    public void Activate() { }
    public void Logic(Vector3 startPos, Vector3 targetPos) { }
    public void Deactivate() { }
    public void Tick() { }
}
