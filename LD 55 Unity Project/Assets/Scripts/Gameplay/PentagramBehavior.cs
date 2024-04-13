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

    private bool clicked = false;
    private void OnTriggerEnter(Collider other)
    {
        glow.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!clicked && _clickAction.action.ReadValue<float>() > 0)
        {
            clicked = true;
            Debug.Log("test");
            SceneManager.LoadScene(sceneName);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        glow.SetActive(false);
    }
}
