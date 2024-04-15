using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Game State")]
public class GameState : ScriptableObject
{
    public DifficultySetting CurrentDifficulty { get; private set; }
    public List<int> CompletedPortals => _completedPortals;
    public int NumberOfCompletedPortals => CompletedPortals.Count;
    public TimeSpan RunTime => _runEndTime - _runStartTime;
    public int CurrentPortal => _currentPortal;

    [Scene]
    [SerializeField]
    List<string> _sceneOrder = new List<string>();

    [Scene]
    [SerializeField]
    string _finalScene;

    int _currentPortal = -1;
    List<int> _completedPortals = new();
    List<KeyValuePair<AbilityType, int>> _abilityRanks = new();

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
            _abilityRanks.Add(new KeyValuePair<AbilityType, int>(type, 0));
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
        if (successful)
        {
            LevelUpAbility();
            _completedPortals.Add(_currentPortal);
        }
    }

    public int GetRankOfAbility(AbilityType abilityType)
    {
        var (ability, rank) = _abilityRanks.FirstOrDefault(x => x.Key == abilityType);
        Debug.Log($"{abilityType} ability is rank {rank}");
        return rank;
    }
    public int GetRankOfAbility(int abilityIndex)
    {
        AbilityType abilityType = (AbilityType)abilityIndex;
        return GetRankOfAbility(abilityType);
    }

    public void LevelUpAbility()
    {
        var indexToLevel = NumberOfCompletedPortals % _abilityRanks.Count;
        var (ability, rank) = _abilityRanks[indexToLevel];
        var newPair = new KeyValuePair<AbilityType, int>(ability, ++rank);
        _abilityRanks[indexToLevel] = newPair;

        Debug.Log($"Leveled up {ability} to {rank}");
    }

    public void CompleteRun() => _runEndTime = DateTime.Now;

    public string GetNextScene()
    {
        if (CurrentDifficulty == DifficultySetting.Hard) return _finalScene;

        var sceneIndex = NumberOfCompletedPortals % _sceneOrder.Count;
        return _sceneOrder[sceneIndex];
    }
}