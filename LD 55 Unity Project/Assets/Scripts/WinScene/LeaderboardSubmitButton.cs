using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardSubmitButton : MonoBehaviour
{
    [SerializeField]
    TMP_InputField _nameInput;

    // Start is called before the first frame update
    void Start()
    {
        _nameInput.onValueChanged.AddListener(HandleNameChanged);
        GetComponent<Button>().onClick.AddListener(SubmitScore);

        SetButtonActive(false);
    }

    void HandleNameChanged(string name)
    {
        SetButtonActive(!string.IsNullOrEmpty(name));
    }

    void SubmitScore()
    {
        LeaderboardWebRequests.Instance.SubmitScore(_nameInput.text);
    }

    void SetButtonActive(bool active) => gameObject.SetActive(active);
}