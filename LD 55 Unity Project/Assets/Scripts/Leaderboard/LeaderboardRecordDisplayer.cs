using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardRecordDisplayer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _nameText;

    [SerializeField]
    TextMeshProUGUI _runTimeText;

    [SerializeField]
    TextMeshProUGUI _portalsText;

    public void SetText(LeaderboardRecord record)
    {
        _nameText.text = record.playerName;
        _runTimeText.text = TimeFormatter.GetTimeString(record.runTime);
        _portalsText.text = $"{record.portalsCompleted}/11 portals";
    }
}