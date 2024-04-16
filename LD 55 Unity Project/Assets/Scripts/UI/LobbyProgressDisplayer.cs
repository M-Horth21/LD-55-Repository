using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyProgressDisplayer : MonoBehaviour
{
    [SerializeField]
    GameState _gameState;

    TextMeshProUGUI _text;

    void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _text.text = $"{_gameState.NumberOfCompletedPortals}/11 rituals completed";
    }
}
