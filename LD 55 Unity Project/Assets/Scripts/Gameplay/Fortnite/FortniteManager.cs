using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FortniteManager : MonoBehaviour
{
    public UnityEvent OnGameWin;
    public UnityEvent OnGameLose;

    [SerializeField] Transform storm;
    [SerializeField] float stormShrinkRate;
    [SerializeField] GameState _gameState;

    [Scene]
    [SerializeField]
    string _lobbyScene;

    [SerializeField] List<GameObject> enemies;

    void Start()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (!enemies[i].activeInHierarchy) enemies.RemoveAt(i);
        }
    }

    private void Update()
    {
        float xScale = Mathf.Lerp(storm.transform.localScale.x, 0, Time.deltaTime * stormShrinkRate);
        float zScale = Mathf.Lerp(storm.transform.localScale.z, 0, Time.deltaTime * stormShrinkRate);

        storm.transform.localScale = new Vector3(xScale, storm.transform.localScale.y, zScale);
    }

    public void Lose()
    {
        _gameState.EndActivePortal(false);
        OnGameLose.Invoke();
        StartCoroutine(DelayedSceneChange());
    }

    public void Win()
    {
        _gameState.EndActivePortal(true);
        OnGameWin.Invoke();
        StartCoroutine(DelayedSceneChange());
    }

    public void HandleEnemyKilled(GameObject gameObject)
    {
        enemies.Remove(gameObject);

        if (enemies.Count == 0) Win();
    }

    IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(_lobbyScene);
    }
}
