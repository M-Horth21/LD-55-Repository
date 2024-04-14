using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    [SerializeField] FortniteManager manager;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask enemyLayer;
    private void OnTriggerExit(Collider other)
    {
        if ((playerLayer & (1 << other.gameObject.layer)) != 0)
        {
            manager.Lose();
        }
        if ((enemyLayer & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(other.gameObject);
        }
    }
}
