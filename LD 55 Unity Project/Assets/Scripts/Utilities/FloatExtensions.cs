using UnityEngine;

public static class FloatExtensions
{
  /// <summary>
  /// Take a float value's relative position in one range, and return the value at the same relative position in a second range.
  /// </summary>
  /// <param name="value">incoming value</param>
  /// <param name="fromMininum">first range minimum</param>
  /// <param name="fromMaximum">first range maximum</param>
  /// <param name="toMinimum">second range minimum</param>
  /// <param name="ToMaximum">second range maximum</param>
  /// <returns></returns>
  public static float Remap(this float value, float fromMininum, float fromMaximum, float toMinimum, float ToMaximum, bool clamp = false)
  {
    if (clamp)
    {
      return Mathf.Clamp01((value - fromMininum) / (fromMaximum - fromMininum)) * (ToMaximum - toMinimum) + toMinimum;
    }
    return (value - fromMininum) / (fromMaximum - fromMininum) * (ToMaximum - toMinimum) + toMinimum;
  }

  /// <summary>
  /// Take a int value's relative position in one range, and return the value at the same relative position in a second range.
  /// </summary>
  /// <param name="value">incoming value</param>
  /// <param name="fromMininum">first range minimum</param>
  /// <param name="fromMaximum">first range maximum</param>
  /// <param name="toMinimum">second range minimum</param>
  /// <param name="ToMaximum">second range maximum</param>
  /// <returns></returns>
  public static float Remap(this int value, float fromMininum, float fromMaximum, float toMinimum, float ToMaximum)
  {
    return (value - fromMininum) / (fromMaximum - fromMininum) * (ToMaximum - toMinimum) + toMinimum;
  }

  public static float RoundToNearestMultipleOf(this float value, float multiplier)
  {
    int divided = Mathf.RoundToInt(value / multiplier);
    return divided * multiplier;
  }

  public static float RoundUpToNearestMultipleOf(this float value, float multiplier)
  {
    int divided = Mathf.CeilToInt(value / multiplier);
    return divided * multiplier;
  }

  public static float RoundDownToNearestMultipleOf(this float value, float multiplier)
  {
    int divided = Mathf.FloorToInt(value / multiplier);
    return divided * multiplier;
  }

  public static bool IsMultipleOf(this float value, float multiplier)
  {
    return value % multiplier == 0;
  }
}