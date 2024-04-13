using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeaderboardRecord
{
    public static LeaderboardRecord Parse(string json)
    {
        return JsonUtility.FromJson<LeaderboardRecord>(json);
    }

    // camelCase!! Or it won't work.
    // And public fields, not properties with getters and setters.
    // And System.Serializable!
    public string id;
    public string playerName;
    public ulong runTime;
    public string badge;

    public string Stringify()
    {
        return JsonUtility.ToJson(this);
    }
}

[System.Serializable]
public class LeaderboardRecordList
{
    public static LeaderboardRecordList Parse(string json)
    {
        return JsonUtility.FromJson<LeaderboardRecordList>(json);
    }

    public List<LeaderboardRecord> leaderboardRecords;

    public string Stringify()
    {
        return JsonUtility.ToJson(this);
    }
}