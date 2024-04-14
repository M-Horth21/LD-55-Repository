using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;

    [SerializeField] CaptureTheZoneManager captureTheZoneManager;
    private void OnTriggerEnter(Collider other)
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            captureTheZoneManager.SetInZone(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            captureTheZoneManager.SetInZone(false);
        }
    }
}
