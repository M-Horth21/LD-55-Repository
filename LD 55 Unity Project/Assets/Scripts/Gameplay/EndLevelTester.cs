using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelTester : MonoBehaviour
{
    [SerializeField]
    GameState _gameState;

    [Scene]
    [SerializeField]
    string _lobbyScene;

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Win"))
        {
            _gameState.EndActivePortal(true);
            SceneManager.LoadScene(_lobbyScene);
        }

        if (GUILayout.Button("Lose"))
        {
            _gameState.EndActivePortal(false);
            SceneManager.LoadScene(_lobbyScene);
        }

        GUILayout.EndHorizontal();
    }
}