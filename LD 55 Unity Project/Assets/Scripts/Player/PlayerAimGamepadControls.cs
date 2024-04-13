using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimGamepadControls : MonoBehaviour
{
  [SerializeField]
  InputActionReference _aimAction;

  [SerializeField]
  AgentMotion _agentMotion;

  // Fires every frame.
  void Update()
  {
    Vector2 stickDirection = _aimAction.action.ReadValue<Vector2>();
    if (stickDirection == Vector2.zero && _agentMotion.MotionInput == Vector2.zero)
    {
      _agentMotion.AimInput =
        _agentMotion.transform.position + _agentMotion.transform.forward;
    }
    else
    {
      if (stickDirection != Vector2.zero)
      {
        _agentMotion.AimInput =
          _agentMotion.transform.position + new Vector3(stickDirection.x, 0, stickDirection.y);
      }
      else
      {
        _agentMotion.AimInput =
          _agentMotion.transform.position + new Vector3(_agentMotion.MotionInput.x, 0, _agentMotion.MotionInput.y);
            }
    }
  }
}