using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingLight : MonoBehaviour
{

    [SerializeField] float pulseDuration = 2f;
    [SerializeField] float minIntensity = 1f;
    [SerializeField] float maxIntensity = 2f;
    Light light;
    void Awake()
    {
        light = GetComponent<Light>();
        light.intensity = minIntensity;
    }
    private void Start()
    {
        StartCoroutine(Pulse());
    }


    IEnumerator Pulse()
    {
        while (true)
        {
            while(light.intensity < (maxIntensity - .3f))
            {
                light.intensity = Mathf.Lerp(light.intensity, maxIntensity, Time.deltaTime / pulseDuration);
                yield return null;
            }

            while (light.intensity > (minIntensity + .3f))
            {
                light.intensity = Mathf.Lerp(light.intensity, minIntensity, Time.deltaTime / pulseDuration);
                yield return null;
            }
        }
    }
}
