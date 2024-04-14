using UnityEngine;

  public static class AudioLevelFunctions
  {
    const float _minimumDecibels = -80;

    public static float PercentToDecibels(float percent)
    {
      if (percent <= 0) return _minimumDecibels;
      return Mathf.Log10(percent) * 20;
    }

    public static float DecibelsToPercent(float decibels)
    {
      if (decibels == _minimumDecibels) return 0;
      return Mathf.Pow(10, decibels / 20);
    }
  }