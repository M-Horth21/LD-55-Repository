using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SheepQuestManager : MonoBehaviour
{

    [SerializeField] List<GameObject> orbs;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject lrPrefab;

    List<LineRenderer> lineRenderers = new();

    [SerializeField] List<EnemyMovement> enemyMovements;
    Transform closestOrb;

    [SerializeField] OrbZone orbZone;

    [SerializeField] PunchEnemy enemyToTargetOrb;
    [SerializeField] EnemyMovement enemyToTargetOrbMovement;


    [Header("UI Stuff")]
    [SerializeField] Slider progressBar;

    int origOrbCount;
    int orbsCaptured = 0;
    void Awake()
    {
        foreach(GameObject orb in orbs)
        {
            GameObject obj = Instantiate(lrPrefab, transform);
            LineRenderer lr = obj.GetComponent<LineRenderer>();
            lineRenderers.Add(lr);
        }

        origOrbCount = orbs.Count;

        StartCoroutine(UpdateEnemyTarget());
    }

    void Update()
    {
        for(int i = 0; i < orbs.Count; i++)
        {
            Vector3[] positions = new Vector3[2];
            positions[0] = playerTransform.position;
            positions[1] = orbs[i].transform.position;

            lineRenderers[i].SetPositions(positions);
        }

    }


    public void UpdateScore(GameObject orb)
    {

        orbs.Remove(orb);

        foreach(LineRenderer lr in lineRenderers)
        {
            lr.SetPositions(new Vector3[0]);
        }

        if(progressBar.value >= .98f)
        {
            Debug.Log("You Win!");
        }

        orbsCaptured++;


        progressBar.value = orbsCaptured / (float)origOrbCount;
    }
    IEnumerator UpdateEnemyTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);

            GameObject closestOrbObj = null;
            float minDist = Mathf.Infinity;

            foreach(GameObject orb in orbs)
            {
                float dist = (orb.transform.position - playerTransform.position).sqrMagnitude;
                if(dist < minDist)
                {
                    minDist = dist;
                    closestOrbObj = orb;
                }
            }

            if(closestOrbObj == null)
            {
                Debug.Log("you win I guess...?");
                yield break;
            }
            closestOrb = closestOrbObj.transform;


            foreach(EnemyMovement enemyMovement in enemyMovements)
            {
                enemyMovement.SetTarget(closestOrb);
            }
            enemyToTargetOrb.SetTarget(closestOrb);
            enemyToTargetOrbMovement.SetPlayer(closestOrb);
        }
    }
}
