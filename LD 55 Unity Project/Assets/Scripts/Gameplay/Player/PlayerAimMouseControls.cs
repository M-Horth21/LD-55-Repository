using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimMouseControls : MonoBehaviour
{
  [SerializeField]
  AgentMotion _agentMotion;

  [SerializeField]
  LayerMask _layerMask;

  [SerializeField]
  InputActionReference _aimAction;

  [SerializeField]
  Transform _crosshair;

  // Only fires once, even if object is re-enabled later.
  void Awake()
  {
    // If an agent motion hasn't already been assigned, try to get one.
    if (_agentMotion == null) _agentMotion = GetComponent<AgentMotion>();
  }

  // Fires every frame.
  void Update()
  {
    Vector2 mousePos = _aimAction.action.ReadValue<Vector2>();
    var ray = Camera.main.ScreenPointToRay(mousePos);

    // Find where on the play plane the mouse is hovering.
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask,
      QueryTriggerInteraction.Collide))
    {
      //Debug.Log($"Mouse position raycast from camera landed at {hit.point}");
      _crosshair.position = hit.point;
    }

    _agentMotion.AimInput = _crosshair.position;
  }
}