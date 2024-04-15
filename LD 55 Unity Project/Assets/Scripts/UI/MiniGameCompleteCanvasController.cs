using TMPro;
using UnityEngine;

public class MiniGameCompleteCanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject _panel;

    [SerializeField]
    TextMeshProUGUI _resultText;

    // Start is called before the first frame update
    void Start()
    {
        _panel.SetActive(false);
    }

    public void ShowPanel(bool success)
    {
        _panel.SetActive(true);
        _resultText.text = success ? "You've completed this portal" : "You lost... try again";
    }
}