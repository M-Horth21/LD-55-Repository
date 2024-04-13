using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptureTheZoneManager : MonoBehaviour
{

    [SerializeField] float timeRequired = 10f;
    [SerializeField] float loseRate = 10f;
    [SerializeField] Slider progressBar;
    [SerializeField] GameObject zone;
    Material zoneMat;

    private float progress = 0f;
    private bool inZone = false;


    private void Awake()
    {
        zoneMat = zone.GetComponent<Renderer>().material;
    }
    void Update()
    {
        if (inZone)
        {
            progress += Time.deltaTime / timeRequired;
        }
        else
        {
            progress -= Time.deltaTime * loseRate;

        }

        progress = Mathf.Clamp(progress, 0f, 1f);
        progressBar.value = progress;

        zoneMat.SetFloat("_FresnelProgress", progress);
    }

    public void SetInZone(bool inZone)
    {
        this.inZone = inZone;
    }

}
