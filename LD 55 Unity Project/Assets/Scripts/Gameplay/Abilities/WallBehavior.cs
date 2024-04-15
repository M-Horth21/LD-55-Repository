using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{

    private float growTime = .1f;
    private float shrinkTime = .15f;

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0 );
        transform.position = new Vector3(transform.position.x, -2, transform.position.z);
        StartCoroutine(Grow());
    }

    void Destroy()
    {
        StartCoroutine(DestroyObj());
    }

    IEnumerator DestroyObj()
    {
        while (transform.position.y > -1.99f)
        {
            float yPos = Mathf.Lerp(transform.position.y, -2.5f, Time.deltaTime / shrinkTime);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator Grow()
    {
        while (transform.position.y < -.1)
        {
            float yPos = Mathf.Lerp(transform.position.y, 0f, Time.deltaTime / growTime);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            yield return null;
        }
    }

    public void SetSettings(WallSettings settings)
    {
        Invoke("Destroy", settings.wallDuration);
    }

}
