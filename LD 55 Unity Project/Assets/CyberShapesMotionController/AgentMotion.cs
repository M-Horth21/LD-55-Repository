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

  Rigidbody2D _rigidbody;

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

  Vector2 _aimInput = new Vector2(-1, 0);
  public Vector2 AimInput
  {
    get { return _aimInput; }
    set
    {
      _aimInput = value;
    }
  }

  public Vector2 CurrentVelocity => _rigidbody.velocity;

  // Only fires once, even if object is re-enabled later.
  void Awake()
  {
    _rigidbody = GetComponent<Rigidbody2D>();
  }

  void FixedUpdate()
  {
    Vector2 targetVelocity = MotionInput * _baseSpeed;

    _rigidbody.velocity = Vector2.MoveTowards(
      _rigidbody.velocity,
      targetVelocity,
      _acceleration * Time.fixedDeltaTime);

    // Aim input will be a world space coordinate to look at.
    Vector2 lookVector = _aimInput - (Vector2)transform.position;
    Quaternion lookRotation = Quaternion.RotateTowards(
      transform.rotation,
      Quaternion.LookRotation(Vector3.forward, lookVector),
      _aimSpeed * Time.fixedDeltaTime);
    _rigidbody.MoveRotation(lookRotation);
  }
}