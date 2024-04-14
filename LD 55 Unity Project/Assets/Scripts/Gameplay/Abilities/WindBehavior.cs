using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBehavior : MonoBehaviour
{
    LayerMask affectedObjects;

    private float windStrength = 0f;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if ((affectedObjects & (1 << other.gameObject.layer)) != 0)
        {
            Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();

            Vector3 force = transform.parent.forward * windStrength;

            otherRB.AddForce(force, ForceMode.VelocityChange);
            //Debug.Log($"{otherRB.velocity.x}, {otherRB.velocity.z}");

            //Debug.Log($"{force.x}, {force.z}");
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawLine(transform.position, transform.parent.forward * windStrength);
    //}

    public void SetSettings(WindSettings settings)
    {
        windStrength = settings.windStrength;
        affectedObjects = settings.affectedLayers;
    }
}
