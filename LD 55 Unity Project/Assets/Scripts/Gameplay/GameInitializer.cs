using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    GameState _gameState;

    // Start is called before the first frame update
    void Start()
    {
        _gameState.Initialize();
    }
}
