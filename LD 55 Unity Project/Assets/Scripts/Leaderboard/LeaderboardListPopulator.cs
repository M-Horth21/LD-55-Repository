using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardListPopulator : MonoBehaviour
{
    [SerializeField]
    LeaderboardRecordDisplayer _recordPrefab;

    private void OnEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        LeaderboardWebRequests.OnLeaderboardRecordsFetched -= PopulateLeaderboard;
        LeaderboardWebRequests.OnLeaderboardRecordsFetched += PopulateLeaderboard;
    }

    public void PopulateLeaderboard(LeaderboardRecordList list)
    {
        foreach (var record in list.leaderboardRecords)
        {
            var newRecord = Instantiate(_recordPrefab, transform);
            newRecord.SetText(record);
        }
    }
}
