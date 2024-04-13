using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PunchAbility : IAbility
{
    GameObject punchObject;


    private float _recharge;
    public float recharge {
        get
        {
            return _recharge;
        }
    }

    bool recharging = false;
    bool active = false;

    float rechargeTime;

    float currRecharge = 0;
    public PunchAbility(PunchSettings punchSettings)
    {
        this.punchObject = punchSettings.punchPrefab;
        this.rechargeTime = punchSettings.rechargeTime;
    }
    public void Activate()
    {
        active = true;
        recharging = false;
    }
    public void Logic(Vector3 mousePos)
    {
        // nothing here for punch. It's a single frame action.
    }
    public void Deactivate()
    {
        active = false;
        recharging = true;
    }
    public void Tick()
    {
        if (recharging)
        {
            _recharge = Mathf.Clamp01(currRecharge / rechargeTime);
            currRecharge += Time.deltaTime;
            if (_recharge >= .98f) recharging = false;
        }
        else if(active)
        {

            // this means we're either active or charged. 
        }
    }


}
