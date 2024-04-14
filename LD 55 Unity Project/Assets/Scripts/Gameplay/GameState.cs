using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game State")]
public class GameState : ScriptableObject
{
    public DifficultySetting CurrentDifficulty { get; private set; }
    public List<int> CompletedPortals => _completedPortals;

    int _currentPortal = -1;
    List<int> _completedPortals = new();

    public void Initialize()
    {
        _completedPortals.Clear();
        _currentPortal = -1;
    }

    public void BeginPortal(int portalId, DifficultySetting portalDifficulty)
    {
        _currentPortal = portalId;
        CurrentDifficulty = portalDifficulty;
    }

    public void EndActivePortal(bool successful)
    {
        if (successful) _completedPortals.Add(_currentPortal);
        _currentPortal = -1;
    }
}