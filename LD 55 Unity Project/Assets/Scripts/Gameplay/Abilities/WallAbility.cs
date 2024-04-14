using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAbility : IAbility
{
    WallSettings settings;
    LayerMask wallLayer;
    GameObject wallPrefab;

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

    public WallAbility(WallSettings wallSettings)
    {
        this.settings = wallSettings;


        this.wallLayer = wallSettings.wallLayer;
        this.wallPrefab = wallSettings.wallPrefab;
        this.rechargeTime = wallSettings.rechargeTime;
        this.abilityTime = wallSettings.abilityTime;
        _recharge = 1;
        currRecharge = abilityTime;

    }
    public void Activate()
    {
        if (active) return;

        AudioManager.instance.PlaySound("WallBuild");

        currRecharge = abilityTime;


        active = true;
        recharging = false;
    }
    public void Logic(Vector3 startPos, Vector3 targetPos)
    {
        if (!active) return;

        RaycastHit hit;



        if(Physics.SphereCast(new Vector3(targetPos.x, targetPos.y + 5, targetPos.z), .3f, Vector3.down, out hit, 5, wallLayer))
        {
        }
        else
        {
            GameObject wall = GameObject.Instantiate(wallPrefab, targetPos, Quaternion.identity);
            wall.GetComponent<WallBehavior>().SetSettings(settings);
        }

        if (currRecharge < .01)
        {
            Deactivate();
        }
    }
    public void Deactivate()
    {
        AudioManager.instance.StopSound("WallBuild");

        currRecharge = (_recharge * rechargeTime);
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
