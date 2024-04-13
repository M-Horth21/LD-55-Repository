using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{

    [SerializeField] CaptureTheZoneManager captureTheZoneManager;
    private void OnTriggerEnter(Collider other)
    {
        captureTheZoneManager.SetInZone(true);
    }
    private void OnTriggerExit(Collider other)
    {
        captureTheZoneManager.SetInZone(false);
    }
}
