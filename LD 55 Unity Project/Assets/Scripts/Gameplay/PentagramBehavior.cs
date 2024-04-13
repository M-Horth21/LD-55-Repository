using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PentagramBehavior : MonoBehaviour
{
    [Scene]
    [SerializeField] string sceneName;

    [SerializeField]
    InputActionReference _clickAction;

    [SerializeField] GameObject glow;

    private void OnTriggerEnter(Collider other)
    {
        glow.SetActive(true);
        _clickAction.action.performed += HandleEnter;
    }

    private void OnTriggerExit(Collider other)
    {
        glow.SetActive(false);
        _clickAction.action.performed -= HandleEnter;
    }

    private void HandleEnter(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(sceneName);
    }
}