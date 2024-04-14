using System.Collections.Generic;
using UnityEngine;

public class AgentMotion : MonoBehaviour
{
  [SerializeField]
  float _baseSpeed = 10;

  [SerializeField]
  float _acceleration = 80;

  [SerializeField]
  float _aimSpeed = 540;

  Rigidbody _rigidbody;

  Vector2 _motionInput;
  public Vector2 MotionInput
  {
    get { return _motionInput; }
    set
    {
      if (value.sqrMagnitude > 1)
      {
        value.Normalize();
      }
      _motionInput = value;
    }
  }

  Vector3 _aimInput = new Vector3(-1, 0, 0);
  public Vector3 AimInput
  {
    get { return _aimInput; }
    set
    {
      _aimInput.x = value.x;
      _aimInput.y = 0;
      _aimInput.z = value.z;
        }
  }

  public Vector2 CurrentVelocity => _rigidbody.velocity;

  // Only fires once, even if object is re-enabled later.
  void Awake()
  {
    _rigidbody = GetComponent<Rigidbody>();
  }

  void FixedUpdate()
  {
        Vector3 targetVelocity = new Vector3(MotionInput.x * _baseSpeed, 0, MotionInput.y * _baseSpeed);

        _rigidbody.velocity = Vector3.MoveTowards(
          _rigidbody.velocity,
          targetVelocity,
          _acceleration * Time.fixedDeltaTime);



        // Aim input will be a world space coordinate to look at.
        Vector3 lookVector = _aimInput - transform.position;
        lookVector.y = 0;
    Quaternion lookRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookVector, Vector3.up), _aimSpeed * Time.fixedDeltaTime);
    _rigidbody.MoveRotation(lookRotation);
  }
}