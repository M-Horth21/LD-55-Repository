using UnityEngine;

public static class ColorExensions
{
  /// <summary>
  /// Edit the alpha value of a color
  /// </summary>
  /// <param name="newAlpha">Alpha is 0 (transparent) to 1 (opaque)</param>
  public static void SetAlphaSelf(ref this Color baseColor, float newAlpha = 1)
  {
    //create a new color object with the color passed in, but change the alpha
    Color _newColor = new Color(baseColor.r, baseColor.g, baseColor.b, newAlpha);
    baseColor = _newColor;
  }

  /// <summary>
  /// Edit the alpha value of a color and return a new color
  /// </summary>
  /// <param name="newAlpha">Alpha is 0 (transparent) to 1 (opaque)</param>
  public static Color NewColorWithAlpha(this Color baseColor, float newAlpha = 1)
  {
    //create a new color object with the color passed in, but change the alpha
    Color _newColor = new Color(baseColor.r, baseColor.g, baseColor.b, newAlpha);
    return _newColor;
  }
}