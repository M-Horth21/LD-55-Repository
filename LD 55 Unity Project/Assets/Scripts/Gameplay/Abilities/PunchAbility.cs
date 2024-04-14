using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PunchAbility : IAbility
{
    GameObject punchPrefab;

    GameObject currPunch;

    PunchSettings settings;

    private float _recharge;
    public float recharge {
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

    public PunchAbility(PunchSettings punchSettings)
    {
        this.settings = punchSettings;

        this.punchPrefab = punchSettings.punchPrefab;
        this.rechargeTime = punchSettings.rechargeTime;
        this.abilityTime = punchSettings.abilityTime;
        _recharge = 1;
        currRecharge = abilityTime;

    }
    public void Activate()
    {
        if (active) return;

        currRecharge = abilityTime;

        currPunch = GameObject.Instantiate(punchPrefab);

        currPunch.GetComponentInChildren<PunchBehavior>().SetSettings(settings);

        active = true;
        recharging = false;
    }
    public void Logic(Vector3 startPos, Vector3 targetPos)
    {
        if (!active) return;

        currPunch.transform.position = startPos;
        currPunch.transform.rotation = Quaternion.LookRotation(targetPos - startPos);

        if (currRecharge < .01)
        {
            Deactivate();
        }
    }
    public void Deactivate()
    {
        GameObject.Destroy(currPunch);
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