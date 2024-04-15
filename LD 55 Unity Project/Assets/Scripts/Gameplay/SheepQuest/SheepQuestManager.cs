using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SheepQuestManager : MonoBehaviour
{
    public UnityEvent OnGameWin;
    public UnityEvent OnGameLose;

    [SerializeField] List<GameObject> orbs;
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject lrPrefab;
    [SerializeField] GameState _gameState;

    [Scene]
    [SerializeField]
    string _lobbyScene;

    [Scene]
    [SerializeField]
    string _winScene;

    List<LineRenderer> lineRenderers = new();

    [SerializeField] List<EnemyMovement> enemyMovements;
    Transform closestOrb;

    [SerializeField] OrbZone orbZone;

    [SerializeField] PunchEnemy enemyToTargetOrb;
    [SerializeField] EnemyMovement enemyToTargetOrbMovement;

    [SerializeField]
    List<GameObject> _mediumEnemies;

    [SerializeField]
    List<GameObject> _mediumOrbs;

    [SerializeField]
    List<GameObject> _hardEnemies;

    [SerializeField]
    List<GameObject> _hardOrbs;

    [Header("UI Stuff")]
    [SerializeField] Slider progressBar;

    int origOrbCount;
    int orbsCaptured = 0;
    bool _gameEnded = false;

    void Awake()
    {
        if (_gameState.CurrentDifficulty == DifficultySetting.Medium)
        {
            foreach (var enemy in _mediumEnemies)
            {
                enemy.SetActive(true);
            }

            foreach (var orb in _mediumOrbs)
            {
                orb.SetActive(true);
                orbs.Add(orb);
            }
        }

        if (_gameState.CurrentDifficulty == DifficultySetting.Hard)
        {
            foreach (var enemy in _hardEnemies)
            {
                enemy.SetActive(true);
            }

            foreach (var orb in _hardOrbs)
            {
                orb.SetActive(true);
                orbs.Add(orb);
            }
        }

        foreach (GameObject orb in orbs)
        {
            GameObject obj = Instantiate(lrPrefab, transform);
            LineRenderer lr = obj.GetComponent<LineRenderer>();
            lineRenderers.Add(lr);
        }

        origOrbCount = orbs.Count;

        StartCoroutine(UpdateEnemyTarget());
    }

    void Update()
    {
        for (int i = 0; i < orbs.Count; i++)
        {
            Vector3[] positions = new Vector3[2];
            positions[0] = playerTransform.position;
            positions[1] = orbs[i].transform.position;

            lineRenderers[i].enabled = true;

            lineRenderers[i].SetPositions(positions);
        }

    }


    public void UpdateScore(GameObject orb)
    {

        orbs.Remove(orb);

        foreach (LineRenderer lr in lineRenderers)
        {
            lr.enabled = false;
        }

        orbsCaptured++;
        progressBar.value = orbsCaptured / (float)origOrbCount;

        if (!_gameEnded && progressBar.value >= .98f)
        {
            Debug.Log("You Win!");
            Win();
        }
    }
    IEnumerator UpdateEnemyTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);

            GameObject closestOrbObj = null;
            float minDist = Mathf.Infinity;

            foreach (GameObject orb in orbs)
            {
                float dist = (orb.transform.position - playerTransform.position).sqrMagnitude;
                if (dist < minDist)
                {
                    minDist = dist;
                    closestOrbObj = orb;
                }
            }

            if (closestOrbObj == null)
            {
                Debug.Log("you win I guess...?");
                yield break;
            }
            closestOrb = closestOrbObj.transform;


            foreach (EnemyMovement enemyMovement in enemyMovements)
            {
                enemyMovement.SetTarget(closestOrb);
            }
            enemyToTargetOrb.SetTarget(closestOrb);
            enemyToTargetOrbMovement.SetPlayer(closestOrb);
        }
    }


    public void Lose()
    {
        _gameEnded = true;
        _gameState.EndActivePortal(false);
        AudioManager.instance.PlaySound("LevelLose");
        OnGameLose.Invoke();
        StartCoroutine(DelayedSceneChange());
    }

    public void Win()
    {
        _gameEnded = true;
        _gameState.EndActivePortal(true);
        AudioManager.instance.PlaySound("LevelWin");
        OnGameWin.Invoke();
        StartCoroutine(DelayedSceneChange());
    }

    IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(2);

        var sceneTarget = _gameState.CurrentDifficulty == DifficultySetting.Hard ?
            _winScene : _lobbyScene;
        SceneManager.LoadScene(sceneTarget);
    }
}
