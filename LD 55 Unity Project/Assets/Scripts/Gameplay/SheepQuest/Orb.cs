using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlaySound("OrbCollect");
    }
    private void OnTriggerExit(Collider other)
    {
        AudioManager.instance.StopSound("OrbCollect");
    }
}
