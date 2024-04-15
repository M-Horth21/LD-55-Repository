using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMusicPlayer : MonoBehaviour
{
    private void Start()
    {
        AudioManager.instance.StopSong("BattleTheme");
        AudioManager.instance.StartSong("VictoryTheme");
    }
    private void OnDisable()
    {
        AudioManager.instance.StopSong("VictoryTheme");
    }
}
