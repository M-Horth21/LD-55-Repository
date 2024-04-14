using TMPro;
using UnityEngine;

public class GameCompletionController : MonoBehaviour
{
    [SerializeField]
    GameState _gameState;

    [SerializeField]
    TextMeshProUGUI _runTimeDisplay;

    [SerializeField]
    TextMeshProUGUI _portalsCompletedDisplay;

    // Start is called before the first frame update
    void Start()
    {
        _gameState.CompleteRun();

        _runTimeDisplay.text = TimeFormatter.GetTimeString(_gameState.RunTime);
        _portalsCompletedDisplay.text = $"{_gameState.NumberOfCompletedPortals:N0}/11";

        GetLeaderboardRecords();
    }

    public void GetLeaderboardRecords() => LeaderboardWebRequests.Instance.GetAllRecords();
}