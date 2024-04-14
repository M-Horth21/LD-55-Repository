using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameCompleteCanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject _panel;

    [SerializeField]
    GameObject _winText;

    [SerializeField]
    GameObject _loseText;

    // Start is called before the first frame update
    void Start()
    {
        _panel.SetActive(false);
    }

    public void ShowPanel(bool success)
    {
        _panel.SetActive(true);
        _winText.SetActive(success);
        _loseText.SetActive(!success);
    }
}