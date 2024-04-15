using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DashAbility : IAbility
{
    GameObject dashPrefab;
    Rigidbody playerRB;
    float dashForce;

    AgentMotion playerMotion;

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

    public DashAbility(DashSettings dashSettings, Transform playerTransform)
    {
        this.dashForce = dashSettings.dashForce;
        this.dashPrefab = dashSettings.dashPrefab;
        this.rechargeTime = dashSettings.rechargeTime;
        this.abilityTime = dashSettings.abilityTime;
        this.playerRB = playerTransform.GetComponent<Rigidbody>();
        this.playerMotion = playerTransform.GetComponent<AgentMotion>();

        _recharge = 1;
        currRecharge = abilityTime;

    }
    public void Activate()
    {
        if (active) return;

        AudioManager.instance.PlaySound("Dash");
        currRecharge = abilityTime;

        active = true;
        recharging = false;

        playerRB.AddForce(new Vector3(playerMotion.MotionInput.x, 0, playerMotion.MotionInput.y) * dashForce, ForceMode.VelocityChange);

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
        //_recharge = 0;
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
