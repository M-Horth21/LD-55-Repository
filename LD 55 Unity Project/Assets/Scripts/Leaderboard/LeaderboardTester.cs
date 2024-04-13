using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LeaderboardTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeaderboardWebRequests.OnLeaderboardRecordsFetched -= HandleLeaderboardRecords;
        LeaderboardWebRequests.OnLeaderboardRecordsFetched += HandleLeaderboardRecords;
    }

    void HandleLeaderboardRecords(LeaderboardRecordList obj)
    {
        foreach (var item in obj.leaderboardRecords)
        {
            var time = TimeSpan.FromMilliseconds(item.runTime);

            StringBuilder timeDisplay = new();
            if (time.TotalHours >= 1) timeDisplay.Append($"{time.TotalHours:N0}h ");
            timeDisplay.Append($"{time.Minutes:N0}m ");
            timeDisplay.Append($"{time.Seconds + time.Milliseconds / 1000f:F1}s");

            Debug.Log($"{item.playerName} had a time of {timeDisplay}");
        }
    }

    void OnGUI()
    {
        if (GUILayout.Button("Get leaderboard records"))
        {
            LeaderboardWebRequests.Instance.GetAllRecords();
        }
    }
}
