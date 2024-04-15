using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CaptureTheZoneManager : MonoBehaviour
{
    public UnityEvent OnGameWin;
    public UnityEvent OnGameLose;

    [Scene]
    [SerializeField]
    string _lobbyScene;

    [SerializeField] float timeRequired = 10f;
    [SerializeField] float loseRate = 10f;
    [SerializeField] Slider progressBar;
    [SerializeField] GameObject zone;
    [SerializeField] GameState _gameState;
    Material zoneMat;

    private float progress = 0f;
    private bool inZone = false;
    bool _gameEnded = false;


    private void Awake()
    {
        zoneMat = zone.GetComponent<Renderer>().material;
    }
    void Update()
    {
        if (inZone)
        {
            progress += Time.deltaTime / timeRequired;
        }
        else
        {
            progress -= Time.deltaTime * loseRate;

        }

        progress = Mathf.Clamp(progress, 0f, 1f);
        progressBar.value = progress;
        if (!_gameEnded && progress >= 1f) Win();

        zoneMat.SetFloat("_FresnelProgress", progress);
    }

    public void SetInZone(bool inZone)
    {
        this.inZone = inZone;
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
        SceneManager.LoadScene(_lobbyScene);
    }
}