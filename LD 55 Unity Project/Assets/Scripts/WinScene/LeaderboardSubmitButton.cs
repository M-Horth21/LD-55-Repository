using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardSubmitButton : MonoBehaviour
{
    [SerializeField]
    TMP_InputField _nameInput;

    void OnEnable()
    {
        LeaderboardWebRequests.OnScoreSubmitted += HandleScoreSubmitted;
    }

    void OnDisable()
    {
        LeaderboardWebRequests.OnScoreSubmitted -= HandleScoreSubmitted;
    }

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

    void HandleScoreSubmitted()
    {
        LeaderboardWebRequests.Instance.GetAllRecords();
        _nameInput.text = string.Empty;
    }

    void SetButtonActive(bool active) => gameObject.SetActive(active);
}