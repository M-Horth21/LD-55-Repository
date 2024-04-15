using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbZone : MonoBehaviour
{
    [SerializeField] SheepQuestManager gameManager;
    [SerializeField] LayerMask orbLayer;

    Dictionary<GameObject, float> orbsInZone = new();

    [SerializeField] float timeToDestroyOrb = 3f;


    IEnumerator OrbCountdown(Orb orb)
    {
        float timePassed = 0f;

        while (timePassed <= timeToDestroyOrb)
        {
            timePassed += Time.deltaTime;
            yield return null;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if ((orbLayer & (1 << other.gameObject.layer)) != 0)
        {
            if (orbsInZone.ContainsKey(other.gameObject))
            {
                if(orbsInZone[other.gameObject]>= timeToDestroyOrb) {
                    Destroy(other.gameObject);
                    gameManager.UpdateScore(other.gameObject);
                }
                orbsInZone[other.gameObject] += Time.deltaTime;
            }
            else
            {
                orbsInZone.Add(other.gameObject, 0f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((orbLayer & (1 << other.gameObject.layer)) != 0)
        {
            orbsInZone.Remove(other.gameObject);
        }

    }
}
