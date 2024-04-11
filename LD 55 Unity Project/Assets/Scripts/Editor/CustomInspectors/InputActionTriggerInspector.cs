using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InputActionTrigger))]
[CanEditMultipleObjects]
public class InputActionTriggerInspector : Editor
{
  SerializedProperty _actionReference;
  SerializedProperty _newAction;
  SerializedProperty _createNewAction;

  void OnEnable()
  {
    _actionReference = serializedObject.FindProperty("_actionReference");
    _newAction = serializedObject.FindProperty("_newAction");
    _createNewAction = serializedObject.FindProperty("_createNewAction");
  }

  public override void OnInspectorGUI()
  {
    base.OnInspectorGUI();

    _createNewAction.boolValue = EditorGUILayout.Toggle("Create new action", _createNewAction.boolValue);

    if (_createNewAction.boolValue)
    {
      EditorGUILayout.PropertyField(_newAction, new GUIContent("New action"));
    }
    else
    {
      EditorGUILayout.PropertyField(_actionReference, new GUIContent("Input action reference"));
    }

    serializedObject.ApplyModifiedProperties();
  }
}