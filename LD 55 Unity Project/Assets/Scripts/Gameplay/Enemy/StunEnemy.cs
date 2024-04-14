using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunEnemy : MonoBehaviour
{
    [SerializeField] StunSettings stunSettings;
    [SerializeField] Transform playerTransform;

    StunAbility stunAbility;

    private void Awake()
    {
        stunAbility = new StunAbility(stunSettings);
    }
    private void Start()
    {
    }
    private void Update()
    {

        if ((transform.position - playerTransform.position).magnitude < 5f && stunAbility.recharge > .99f)
        {
            stunAbility.Activate();

        }
        stunAbility.Logic(transform.position, transform.position + transform.forward);

        stunAbility.Tick();
    }
}
