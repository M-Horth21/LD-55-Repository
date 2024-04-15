using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepQuestManager : MonoBehaviour
{

    [SerializeField] List<GameObject> orbs;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject lrPrefab;

    List<LineRenderer> lineRenderers = new();


    void Awake()
    {
        foreach(GameObject orb in orbs)
        {
            GameObject obj = Instantiate(lrPrefab, transform);
            LineRenderer lr = obj.GetComponent<LineRenderer>();
            lineRenderers.Add(lr);
        }
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
}
