using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchBehavior : MonoBehaviour
{
    LayerMask affectedObjects;

    private float punchStrength = 0f;
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if ((affectedObjects & (1 << other.gameObject.layer)) != 0)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * punchStrength, ForceMode.Impulse);
        }
    }


    public void SetSettings(PunchSettings settings)
    {
        punchStrength = settings.punchStrength;
        affectedObjects = settings.affectedLayers;
    }
}