using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameHintDisplayer : MonoBehaviour
{
    [SerializeField]
    GameObject _hintPanel;

    void Start()
    {
        _hintPanel.SetActive(true);
        //StartCoroutine(HidePanelDelayed());
    }

    IEnumerator HidePanelDelayed()
    {
        yield return new WaitForSeconds(5f);
        _hintPanel.SetActive(false);
    }
}