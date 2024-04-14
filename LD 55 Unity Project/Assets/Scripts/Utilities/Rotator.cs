using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace OutputEnable.Utilities
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] UpdateType _updateType;
        [SerializeField] TimeScale _timeScale;
        [SerializeField] Space rotSpace = Space.World;

        [HideInInspector][SerializeField] bool _random;
        [HideInInspector][SerializeField] float _randomScalar = 50;
        [HideInInspector][SerializeField] float _rotateSpeedX = 0;
        [HideInInspector][SerializeField] float _rotateSpeedY = 0;
        [HideInInspector][SerializeField] float _rotateSpeedZ = 0;

        float _delta = 0;

        private void Awake()
        {
            if (_random)
            {
                Vector3 _randomized = Random.insideUnitSphere * _randomScalar;
                _rotateSpeedX = _randomized.x;
                _rotateSpeedY = _randomized.y;
                _rotateSpeedZ = _randomized.z;
            }
        }

        void Update()
        {
            if (_updateType != UpdateType.Normal) return;

            _delta = GetDeltaTime();
            transform.Rotate(_rotateSpeedX * _delta, _rotateSpeedY * _delta, _rotateSpeedZ * _delta, rotSpace);
        }

        private void FixedUpdate()
        {
            if (_updateType != UpdateType.Fixed) return;

            _delta = GetDeltaTime();
            transform.Rotate(_rotateSpeedX * _delta, _rotateSpeedY * _delta, _rotateSpeedZ * _delta, rotSpace);
        }

        float GetDeltaTime()
        {
            if (_updateType == UpdateType.Fixed)
            {
                return _timeScale switch
                {
                    TimeScale.Unscaled => Time.fixedUnscaledDeltaTime,
                    _ => Time.fixedDeltaTime
                };
            }

            return _timeScale switch
            {
                TimeScale.Unscaled => Time.unscaledDeltaTime,
                _ => Time.deltaTime
            };
        }

        private enum UpdateType
        {
            Normal,
            Fixed
        }

        private enum TimeScale
        {
            Normal,
            Unscaled
        }
    }

#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Rotator))]
    public class RotatorEditor : Editor
    {
        SerializedProperty _randomValue;
        SerializedProperty _randomScalar;
        SerializedProperty _rotateSpeedX;
        SerializedProperty _rotateSpeedY;
        SerializedProperty _rotateSpeedZ;

        private void OnEnable()
        {
            _randomValue = serializedObject.FindProperty("_random");
            _randomScalar = serializedObject.FindProperty("_randomScalar");
            _rotateSpeedX = serializedObject.FindProperty("_rotateSpeedX");
            _rotateSpeedY = serializedObject.FindProperty("_rotateSpeedY");
            _rotateSpeedZ = serializedObject.FindProperty("_rotateSpeedZ");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Rotator rotator = (Rotator)target;

            serializedObject.Update();

            EditorGUILayout.PropertyField(_randomValue);

            if (!_randomValue.boolValue)
            {
                EditorGUILayout.PropertyField(_rotateSpeedX, new GUIContent("X Rotation"));
                EditorGUILayout.PropertyField(_rotateSpeedY, new GUIContent("Y Rotation"));
                EditorGUILayout.PropertyField(_rotateSpeedZ, new GUIContent("Z Rotation"));
            }
            else
            {
                EditorGUILayout.PropertyField(_randomScalar, new GUIContent("Scale For Random","Random rotation will be generated from 0 to 1, scale it with this multiplier"));
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}