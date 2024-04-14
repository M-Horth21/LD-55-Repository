using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PentagramGlow : MonoBehaviour
{

    [SerializeField] LayerMask playerLayer;
    [SerializeField] Renderer glowRenderer;

    Material glowMaterial;
    private void Awake()
    {
        glowMaterial = glowRenderer.material;
    }
    private void OnTriggerStay(Collider other)
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            float dist = (other.transform.position - transform.position).magnitude;
            Debug.Log(dist);

            float intensity = Mathf.Pow(.5f, (dist - .1f));

            glowMaterial.SetFloat("_Intensity", intensity);

        }

    }
}
