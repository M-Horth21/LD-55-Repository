using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchEnemy : MonoBehaviour
{
    [SerializeField] PunchSettings punchSettings;
    [SerializeField] Transform playerTransform;

    PunchAbility punchAbility;

    private void Awake()
    {
        punchAbility = new PunchAbility(punchSettings);
    }
    private void Start()
    {
    }
    private void Update()
    {

        if(playerTransform == null)
        {
            return;
        }
        if ((transform.position - playerTransform.position).magnitude < 2f && punchAbility.recharge > .99f)
        {
            punchAbility.Activate();

        }
        punchAbility.Logic(transform.position, transform.position + transform.forward);

        punchAbility.Tick();
    }

    public void SetTarget(Transform target)
    {
        playerTransform = target;
    }
}
