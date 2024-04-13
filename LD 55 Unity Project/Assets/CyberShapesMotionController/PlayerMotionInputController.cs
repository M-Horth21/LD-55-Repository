using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotionInputController : MonoBehaviour
{
  [SerializeField]
  InputActionReference _moveAction;

  [SerializeField]
  AgentMotion _agentMotion;

  // Only fires once, even if object is re-enabled later.
  void Awake()
  {
    // If an agent motion hasn't already been assigned, try to get one.
    if (_agentMotion == null) _agentMotion = GetComponent<AgentMotion>();
  }

  // Fires every frame.
  void Update()
  {
    _agentMotion.MotionInput = _moveAction.action.ReadValue<Vector2>();
  }
}