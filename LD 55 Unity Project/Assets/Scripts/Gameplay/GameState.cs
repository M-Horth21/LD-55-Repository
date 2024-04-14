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
    public TimeSpan RunTime => _runEndTime - _runStartTime;

    [Scene]
    [SerializeField]
    List<string> _sceneOrder = new List<string>();

    [Scene]
    [SerializeField]
    string _finalScene;

    int _currentPortal = -1;
    List<int> _completedPortals = new();
    Dictionary<AbilityType, int> _abilityRanks = new();

    DateTime _runStartTime;
    DateTime _runEndTime;

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        _completedPortals.Clear();
        _abilityRanks.Clear();

        _currentPortal = -1;

        foreach (AbilityType type in Enum.GetValues(typeof(AbilityType)))
        {
            _abilityRanks.Add(type, 0);
        }

        _runStartTime = DateTime.Now;
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

    public void CompleteRun() => _runEndTime = DateTime.Now;

    public string GetNextScene()
    {
        if (CurrentDifficulty == DifficultySetting.Hard) return _finalScene;

        var sceneIndex = NumberOfCompletedPortals % _sceneOrder.Count;
        return _sceneOrder[sceneIndex];
    }
}