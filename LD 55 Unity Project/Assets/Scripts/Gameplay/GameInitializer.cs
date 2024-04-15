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
        AudioManager.instance.StartSong("MenuTheme");
        _gameState.Initialize();
    }
    private void OnDestroy()
    {
        AudioManager.instance.StopSong("MenuTheme");

        AudioManager.instance.StartSong("BattleTheme");
    }
}
