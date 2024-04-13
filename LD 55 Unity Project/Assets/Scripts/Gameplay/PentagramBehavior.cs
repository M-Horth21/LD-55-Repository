using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;
using UnityEngine.UI;

public class PentagramBehavior : MonoBehaviour
{
    /// <summary>
    /// This likely gets replaced with scriptable object configuration later.
    /// </summary>
    enum Difficulty
    {
        Easy,
        Medium,
        Hard
    }

    [Scene]
    [SerializeField] string sceneName;

    [SerializeField]
    TextMeshProUGUI _tipText;

    [SerializeField]
    GameObject _tipCanvas;

    [SerializeField]
    Difficulty _difficulty = Difficulty.Easy;

    [SerializeField]
    float _captureTime = 3;

    [SerializeField]
    Slider _progressSlider;

    float _captureProgress = 0;
    bool _capturing = false;

    private void Awake()
    {
        _tipText.text = $"Entering {_difficulty} level";
        _tipCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        _captureProgress = 0;
        _capturing = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _capturing = false;
        _captureProgress = 0;
    }

    private void Update()
    {
        if (!_capturing) return;

        _captureProgress += Time.deltaTime;
        _progressSlider.value = _captureProgress / _captureTime;

        if (_captureProgress >= _captureTime) SceneManager.LoadScene(sceneName);
    }
}