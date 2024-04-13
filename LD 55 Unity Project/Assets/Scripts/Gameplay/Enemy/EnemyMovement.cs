using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] Transform targetPos;
    [Range(0, 1), SerializeField] float preference = .5f;

    [SerializeField] AgentMotion _agentMotion;

   
    Vector3 target; // it's a global variable so I can draw it in the gizmos.
    void Update()
    {

        // 0 preference means the target pos is targeted. 1 means the player pos is targeted.
        Vector3 resultant = (playerPos.position - targetPos.position);
        target = targetPos.position + (preference * resultant);

        Vector2 moveDir = new Vector2(target.x - transform.position.x, target.z - transform.position.z).normalized;
        _agentMotion.MotionInput = moveDir;
        _agentMotion.AimInput = playerPos.position;

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(target, .5f);
        Gizmos.DrawLine(Vector3.zero, playerPos.position);
        Gizmos.DrawLine(Vector3.zero, targetPos.position);
    }
}
