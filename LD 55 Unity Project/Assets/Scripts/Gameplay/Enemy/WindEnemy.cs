using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEnemy : MonoBehaviour
{
    [SerializeField] WindSettings windSettings;
    [SerializeField] Transform playerTransform;

    WindAbility windAbility;

    private void Awake()
    {
        windAbility = new WindAbility(windSettings);
    }
    private void Start()
    {
    }
    private void Update()
    {

        if((transform.position - playerTransform.position).magnitude < 4f && windAbility.recharge > .99f)
        {
            windAbility.Activate();

        }
        windAbility.Logic(transform.position, transform.position + transform.forward);

        windAbility.Tick();
    }
}
