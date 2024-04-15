using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float height;
    [SerializeField] private float followSpeed;

    void Start()
    {
        transform.position = new Vector3(playerTransform.position.x, height, playerTransform.position.z);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerTransform.position.x, height, playerTransform.position.z), followSpeed * Time.deltaTime);
    }
}
