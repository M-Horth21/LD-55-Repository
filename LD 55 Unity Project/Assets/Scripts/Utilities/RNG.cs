using UnityEngine;

public static class RNG
{
  /// <returns>
  /// a true/false if the RNG passes the dice roll based on the provided chance
  /// </returns>
  /// <param name="inChance"> <b>float</b> value from 0 to 1<br/>0 is guaranteed failure.<br/>1 is guaranteed success.</param>
  public static bool RollForChance(float inChance) =>
    Random.Range(0f, 1f) >= (1 - inChance);
}