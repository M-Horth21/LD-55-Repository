using System.Linq;
using UnityEngine;

public class LobbyPlayerPlacer : MonoBehaviour
{
    [SerializeField]
    Rigidbody _player;

    [SerializeField]
    GameState _gameState;

    [SerializeField]
    PortalPopulator _portalPopulator;

    void Awake()
    {
        if (_gameState.CurrentPortal < 0) return;

        var lastPortal = _portalPopulator.Portals
            .Where(p => p.PortalId == _gameState.CurrentPortal).FirstOrDefault();

        Vector3 spawnPos = lastPortal.transform.position + new Vector3(3, 0, 0);
        spawnPos.y = _player.transform.position.y;
        _player.MovePosition(spawnPos);
        _player.transform.position = spawnPos;
    }
}
