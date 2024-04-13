using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAbility : IAbility
{
    GameObject windPrefab;

    private float _recharge;
    public float recharge
    {
        get
        {
            return _recharge;
        }
    }

    float rechargeTime;
    float abilityTime;

    float currRecharge;
    bool recharging = false;
    bool active = false;

    public WindAbility(WindSettings windSettings)
    {
        this.windPrefab = windSettings.windPrefab;
        this.rechargeTime = windSettings.rechargeTime;
        this.abilityTime = windSettings.abilityTime;
        _recharge = 1;
        currRecharge = abilityTime;
    }
    public void Activate()
    {
        if (active)
        {
            return;
        }


        currRecharge = abilityTime;

        active = true;
        recharging = false;
    }
    public void Logic(Vector3 mousePos)
    {
        if (!active)
        {
            return;
        }

        if (currRecharge < .01)
        {
            Deactivate();
        }
    }
    public void Deactivate()
    {

        recharging = true;
        active = false;
    }
    public void Tick()
    {
        if (recharging)
        {
            _recharge = Mathf.Clamp01(currRecharge / rechargeTime);
            currRecharge += Time.deltaTime;
            if (_recharge >= 1f) recharging = false;
        }
        else if (active)
        {
            _recharge = Mathf.Clamp01(currRecharge / abilityTime);
            currRecharge -= Time.deltaTime;
        }
    }
}
