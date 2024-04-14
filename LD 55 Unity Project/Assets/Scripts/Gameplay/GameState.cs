using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game State")]
public class GameState : ScriptableObject
{
    public DifficultySetting CurrentDifficulty { get; private set; }
    public List<int> CompletedPortals => _completedPortals;
    public int NumberOfCompletedPortals => CompletedPortals.Count;

    int _currentPortal = -1;
    List<int> _completedPortals = new();
    Dictionary<AbilityType, int> _abilityRanks = new();

    public void Initialize()
    {
        _completedPortals.Clear();
        _currentPortal = -1;

        foreach (AbilityType type in Enum.GetValues(typeof(AbilityType)))
        {
            _abilityRanks.Add(type, 0);
        }
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

    public int GetRankOfAbility(AbilityType ability) => _abilityRanks[ability];

    public void LevelUpAbility(AbilityType ability) => _abilityRanks[ability]++;
}