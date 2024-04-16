using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public partial class PentagramBehavior : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _tipText;

    [SerializeField]
    GameObject _tipCanvas;

    [SerializeField]
    DifficultySetting _difficulty = DifficultySetting.Easy;

    [SerializeField]
    float _captureTime = 3;

    [SerializeField]
    Slider _progressSlider;

    [SerializeField]
    int _portalId = 0;

    [SerializeField]
    GameState _gameState;

    float _captureProgress = 0;
    bool _capturing = false;


    private void Awake()
    {
        _tipText.text = $"Entering {_difficulty} ritual";
        _tipCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlaySound("Teleport");
        _captureProgress = 0;
        _capturing = true;
    }

    private void OnTriggerExit(Collider other)
    {
        AudioManager.instance.StopSound("Teleport");

        _capturing = false;
        _captureProgress = 0;
    }


    private void Update()
    {
        if (!_capturing) return;

        _captureProgress += Time.deltaTime;
        _progressSlider.value = _captureProgress / _captureTime;

        if (_captureProgress >= _captureTime)
        {
            _gameState.BeginPortal(PortalId, _difficulty);
            SceneManager.LoadScene(_gameState.GetNextScene());
        }
    }

    public int PortalId => _portalId;
}