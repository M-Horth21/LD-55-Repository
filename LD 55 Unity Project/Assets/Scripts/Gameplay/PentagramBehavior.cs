using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using TMPro;

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
    InputActionReference _clickAction;

    [SerializeField] GameObject glow;

    [SerializeField]
    TextMeshProUGUI _tipText;

    [SerializeField]
    GameObject _tipCanvas;

    [SerializeField]
    Difficulty _difficulty = Difficulty.Easy;

    private void Awake()
    {
        glow.SetActive(false);
        _tipCanvas.SetActive(false);
        _tipText.text = $"Press space to enter {_difficulty} level";
    }

    private void OnTriggerEnter(Collider other)
    {
        glow.SetActive(true);
        _tipCanvas.gameObject.SetActive(true);
        _clickAction.action.performed += HandleEnter;
    }

    private void OnTriggerExit(Collider other)
    {
        glow.SetActive(false);
        _tipCanvas.SetActive(false);
        _clickAction.action.performed -= HandleEnter;
    }

    private void HandleEnter(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnDisable()
    {
        _clickAction.action.performed -= HandleEnter;
    }
}