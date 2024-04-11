using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputActionTrigger : MonoBehaviour
{
  [SerializeField]
  UnityEvent _onActionPerformed;

  [SerializeField, HideInInspector]
  InputActionReference _actionReference;

  [SerializeField, HideInInspector]
  InputAction _newAction;

  [SerializeField, HideInInspector]
  bool _createNewAction = false;

  public InputAction Action => _createNewAction ? _newAction : _actionReference.action;

  void Awake()
  {
    if (Action == null) return;

    Action.Enable();
    Action.performed -= HandleActionPerformed;
    Action.performed += HandleActionPerformed;
  }

  void HandleActionPerformed(InputAction.CallbackContext action)
  {
    if (!action.performed) return;

    _onActionPerformed.Invoke();
  }
}