using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Responsible for listening to control scheme change events and enabling/disabling the corresponding control components.
/// </summary>
public class PlayerAimInputController : MonoBehaviour
{
  public enum ControlType
  {
    KeyboardMouse,
    Gamepad
  }

  [SerializeField]
  UnityEvent OnMouseControl;

  [SerializeField]
  UnityEvent OnGamepadControl;

  ControlType _controlType;

  public void HandleControlsChangedEvent(PlayerInput playerInput)
  {
    if (!Enum.TryParse(playerInput.currentControlScheme, out _controlType))
    {
      _controlType = ControlType.KeyboardMouse;
    }

    switch (_controlType)
    {
      case ControlType.KeyboardMouse:
        OnMouseControl.Invoke();
        break;
      case ControlType.Gamepad:
        OnGamepadControl.Invoke();
        break;
      default:
        break;
    }
  }
}