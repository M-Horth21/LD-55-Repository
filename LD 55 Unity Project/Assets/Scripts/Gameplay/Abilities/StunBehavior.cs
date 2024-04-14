using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunBehavior : MonoBehaviour
{

    LayerMask affectedObjects;

    private float growTime = .25f;
    private float shrinkTime = .10f;

    private float stunStrength = 0f;
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(GrowRadius());
    }

    private void OnTriggerStay(Collider other)
    {
        if ((affectedObjects & (1 << other.gameObject.layer)) != 0)
        {
            Rigidbody otherRB = other.GetComponent<Rigidbody>();
            otherRB.velocity *= stunStrength;
        }
    }

    void Destroy()
    {
        StartCoroutine(DestroyObj());
    }

    IEnumerator DestroyObj()
    {
        while (transform.localScale.x > .1f)
        {
            float scale = Mathf.Lerp(transform.localScale.x, 0, Time.deltaTime / shrinkTime);
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator GrowRadius()
    {
        while(transform.localScale.x < .99)
        {
            float scale = Mathf.Lerp(transform.localScale.x, 1f, Time.deltaTime / growTime);
            transform.localScale = new Vector3(scale, 1, scale);
            yield return null;
        }
    }

    public void SetSettings(StunSettings settings)
    {
        affectedObjects = settings.affectedLayers;
        stunStrength = settings.stunStrength;
        Invoke("Destroy", settings.abilityTime);
    }

}
