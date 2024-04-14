using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StunAbility : IAbility
{
    GameObject stunPrefab;

    GameObject currStun;

    StunSettings settings;

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

    public StunAbility(StunSettings stunSettings)
    {
        this.settings = stunSettings;

        this.stunPrefab = stunSettings.stunPrefab;
        this.rechargeTime = stunSettings.rechargeTime;
        this.abilityTime = stunSettings.abilityTime;
        _recharge = 1;
        currRecharge = abilityTime;

    }
    public void Activate()
    {
        if (active) return;

        currRecharge = abilityTime;

        currStun = GameObject.Instantiate(stunPrefab);

        currStun.GetComponent<StunBehavior>().SetSettings(settings);

        active = true;
        recharging = false;
    }
    public void Logic(Vector3 startPos, Vector3 targetPos)
    {
        if (!active) return;

        //currStun.transform.rotation = Quaternion.LookRotation(targetPos - startPos);
        currStun.transform.position = startPos;
        Deactivate();
    }
    public void Deactivate()
    {
        // no need to destroy it. The stun will destroy itself.
        //GameObject.Destroy(currStun);
        currRecharge = 0;
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
