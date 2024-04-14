using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PortalPopulator : MonoBehaviour
{
    [SerializeField]
    GameState _gameState;

    [SerializeField]
    List<PentagramBehavior> _portals = new();

    void Start()
    {
        // Disable any portals that have been completed.
        var completedPortals =
            _gameState.CompletedPortals.Select(
                x => _portals.Where(
                    p => p.PortalId == x).First());

        foreach (var portal in completedPortals)
        {
            portal.gameObject.SetActive(false);
        }
    }
}