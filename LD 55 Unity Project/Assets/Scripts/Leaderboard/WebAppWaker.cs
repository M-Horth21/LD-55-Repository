using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;

public class WebAppWaker : PersistentSingleton<WebAppWaker>
{
    [SerializeField, Tooltip("How much time between wake up calls (seconds)")]
    float _interval = 60;

    // Only fires once, after Awake and OnEnable
    void Start()
    {
        StartCoroutine(RepeatOnInterval());
    }

    IEnumerator RepeatOnInterval()
    {
        while (true)
        {
            StartCoroutine(WakeUp());
            yield return new WaitForSecondsRealtime(_interval);
        }
    }

    IEnumerator WakeUp()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        using (var webRequest = UnityWebRequest.Get($"{LeaderboardWebRequests.RootApiUrl}/alive"))
        {
            yield return webRequest.SendWebRequest();
            UnityEngine.Debug.Log($"Waking up web app service: {webRequest.downloadHandler.text} ({stopwatch.ElapsedMilliseconds}ms)");
        }
    }
}