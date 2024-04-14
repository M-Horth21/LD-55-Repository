using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DashAbility : IAbility
{
    GameObject dashPrefab;
    Rigidbody playerRB;
    float dashForce;

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

    public DashAbility(DashSettings dashSettings, Rigidbody playerRB)
    {
        this.dashForce = dashSettings.dashForce;
        this.dashPrefab = dashSettings.dashPrefab;
        this.rechargeTime = dashSettings.rechargeTime;
        this.abilityTime = dashSettings.abilityTime;
        this.playerRB = playerRB;
        _recharge = 1;
        currRecharge = abilityTime;

    }
    public void Activate()
    {
        if (active) return;

        currRecharge = abilityTime;

        active = true;
        recharging = false;

        playerRB.AddForce(playerRB.velocity.normalized * dashForce, ForceMode.VelocityChange);

        Deactivate();
    }
    public void Logic(Vector3 startPos, Vector3 targetPos)
    {
        if (!active) return;

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
        Debug.Log("dash ticking");
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
