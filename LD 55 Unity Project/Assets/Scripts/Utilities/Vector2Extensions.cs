using UnityEngine;

public static class Vector2Extensions
{
  /// <summary>
  /// Directly rotate a Vector2 by a specified angle
  /// Positive numbers will rotate counter-clockwise
  /// </summary>
  public static void RotateSelf(ref this Vector2 vector, float amount = 0)
  {
    float magnitude = vector.magnitude;
    float startAngle = Mathf.Atan2(vector.y, vector.x);
    startAngle += amount * Mathf.Deg2Rad;
    vector = new Vector2(Mathf.Cos(startAngle), Mathf.Sin(startAngle));
    vector *= magnitude;
  }

  /// <summary>
  /// Rotate a Vector2 by a specified angle
  /// Positive numbers will rotate counter-clockwise
  /// </summary>
  /// <returns>
  /// a new Vector2 value after rotating the given Vector2 by an angle
  /// </returns>
  public static Vector2 RotateNew(this Vector2 vector, float amount = 0)
  {
    float magnitude = vector.magnitude;
    float startAngle = Mathf.Atan2(vector.y, vector.x);
    startAngle += amount * Mathf.Deg2Rad;
    vector = new Vector2(Mathf.Cos(startAngle), Mathf.Sin(startAngle));
    vector *= magnitude;
    return vector;
  }
}