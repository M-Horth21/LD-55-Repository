using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FortniteManager : MonoBehaviour
{
    [SerializeField] Transform storm;
    [SerializeField] float stormShrinkRate;
    [SerializeField] GameState _gameState;

    [Scene]
    [SerializeField]
    string _lobbyScene;

    [SerializeField] List<GameObject> enemies;

    private void Update()
    {
        float xScale = Mathf.Lerp(storm.transform.localScale.x, 0, Time.deltaTime * stormShrinkRate);
        float zScale = Mathf.Lerp(storm.transform.localScale.z, 0, Time.deltaTime * stormShrinkRate);

        storm.transform.localScale = new Vector3(xScale, storm.transform.localScale.y, zScale);
    }

    public void Lose()
    {
        _gameState.EndActivePortal(false);
        SceneManager.LoadScene(_lobbyScene);
    }
    public void Win()
    {
        _gameState.EndActivePortal(true);
        SceneManager.LoadScene(_lobbyScene);

    }

}
