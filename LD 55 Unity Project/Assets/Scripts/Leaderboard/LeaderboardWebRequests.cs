using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class LeaderboardWebRequests : Singleton<LeaderboardWebRequests>
{
    public static List<LeaderboardRecord> AllEntries;
    public static List<LeaderboardRecord> EntriesToShow;
    public static ulong QuickestTime;
    public static LeaderboardRecord PlayersEntryInLeaderboard;
    public static int PlayersRank;

    public static event Action<LeaderboardRecordList> OnLeaderboardRecordsFetched = delegate { };
    public static event Action OnScoreSubmitted = delegate { };
    public static event Action<int> OnRankFound = delegate { };
    public static event Action<LeaderboardRecord> OnPlayerRecordFound = delegate { };

    public UnityEvent OnLeaderboardDisconnect;
    public UnityEvent OnLeaderboardConnect;
    public UnityEvent OnLeaderboardFetch;

    public static string RootApiUrl = "https://ludumdare55.azurewebsites.net/v1/leaderboard";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        AllEntries = new List<LeaderboardRecord>();
        EntriesToShow = new List<LeaderboardRecord>();
        QuickestTime = ulong.MaxValue;
        PlayersEntryInLeaderboard = new LeaderboardRecord();
        PlayersRank = 0;

        OnLeaderboardRecordsFetched = delegate { };
        OnScoreSubmitted = delegate { };
        OnRankFound = delegate { };
        OnPlayerRecordFound = delegate { };
    }

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// gets the top N scores from the leaderboard
    /// </summary>
    public void GetTopScores()
    {
        string endPoint = $"{RootApiUrl}/scores/top/10";
        StartCoroutine(GetScoresCoroutine(endPoint));
    }

    public void GetRankByID(string id)
    {
        string endPoint = $"{RootApiUrl}/scores/{id}/rank";
        StartCoroutine(GetRankByIDCoroutine(endPoint));
    }

    public void GetEntryByID(string id)
    {
        string endPoint = $"{RootApiUrl}/scores/{id}";
        StartCoroutine(GetEntryByIDCoroutine(endPoint));
    }

    public void GetNewID()
    {
        string endPoint = $"{RootApiUrl}/scores/newID";
        StartCoroutine(GetNewIDCoroutine(endPoint));
    }

    public void GetAllRecords()
    {
        string endPoint = $"{RootApiUrl}/scores/";
        StartCoroutine(GetScoresCoroutine(endPoint));
    }

    IEnumerator GetScoresCoroutine(string uri)
    {
        OnLeaderboardFetch.Invoke();

        using (var webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            Debug.Log($"Fetching scores:\n{webRequest.downloadHandler.text}");

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.Success:
                    break;
                default:
                    OnLeaderboardDisconnect.Invoke();
                    Debug.Log(webRequest.error);
                    yield break;
            }

            var recordsList = LeaderboardRecordList.Parse(webRequest.downloadHandler.text);
            AllEntries = recordsList.leaderboardRecords;
            QuickestTime = recordsList.leaderboardRecords[0].runTime;
            OnLeaderboardRecordsFetched.Invoke(recordsList);
        }

    }

    IEnumerator GetRankByIDCoroutine(string uri)
    {
        OnLeaderboardFetch.Invoke();

        UnityWebRequest webRequest = UnityWebRequest.Get(uri);

        yield return webRequest.SendWebRequest();
        Debug.Log($"Fetching player's rank: {webRequest.downloadHandler.text}");

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                OnLeaderboardConnect.Invoke();
                break;
            default:
                OnLeaderboardDisconnect.Invoke();
                yield break;
        }

        PlayersRank = Convert.ToInt32(webRequest.downloadHandler.text);
        OnRankFound.Invoke(PlayersRank);
    }

    IEnumerator GetEntryByIDCoroutine(string uri)
    {
        OnLeaderboardFetch.Invoke();

        UnityWebRequest webRequest = UnityWebRequest.Get(uri);

        yield return webRequest.SendWebRequest();
        Debug.Log($"Fetching player's record:\n{webRequest.downloadHandler.text}");

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                break;
            default:
                OnLeaderboardDisconnect.Invoke();
                yield break;
        }

        var recordsList = LeaderboardRecordList.Parse(webRequest.downloadHandler.text);
        PlayersEntryInLeaderboard = recordsList.leaderboardRecords[0];

        OnPlayerRecordFound.Invoke(PlayersEntryInLeaderboard);
    }

    IEnumerator GetNewIDCoroutine(string uri)
    {
        OnLeaderboardFetch.Invoke();

        UnityWebRequest webRequest = UnityWebRequest.Get(uri);

        yield return webRequest.SendWebRequest();
        Debug.Log($"Fetching new ID: {webRequest.downloadHandler.text}");

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                //OnLeaderboardConnect.Invoke();
                break;
            default:
                OnLeaderboardDisconnect.Invoke();
                yield break;
        }

        //OnNewIDGenerated.Invoke(webRequest.downloadHandler.text);
    }

    public void SubmitScore(ulong runTime, string playerName = "unnamed")
    {
        string endPoint = $"{RootApiUrl}/scores/submit";
        StartCoroutine(SubmitScoreCoroutine(endPoint, runTime, playerName));
    }

    IEnumerator SubmitScoreCoroutine(string uri, ulong runTime, string playerName)
    {
        var webRequest = new UnityWebRequest(uri, "POST");

        // Create the leaderboard record.
        LeaderboardRecord submission = new()
        {
            id = SystemInfo.deviceUniqueIdentifier,
            playerName = playerName,
            runTime = runTime,
            badge = "",
        };

        Debug.Log($"Submitting score record:\n{submission.Stringify()}");

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(submission.Stringify());
        webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();
        Debug.Log($"Score saved in leaderboard:\n{webRequest.downloadHandler.text}");

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                OnScoreSubmitted.Invoke();
                break;
            default:
                OnLeaderboardDisconnect.Invoke();
                yield break;
        }
    }
}