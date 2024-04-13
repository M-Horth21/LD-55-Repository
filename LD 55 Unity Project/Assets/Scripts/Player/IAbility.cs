using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public void Activate();
    public void Logic(Vector3 MousePos);
    public void Deactivate();
    public void Tick();
    public float recharge { get; }
}
