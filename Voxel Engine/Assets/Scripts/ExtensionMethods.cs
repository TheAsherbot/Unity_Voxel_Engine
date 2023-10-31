using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public static class ExtensionMethods
{
    
    /// <summary>
    /// closed match in RGB space
    /// </summary>
    /// <param name="color"></param>
    /// <param name="colors"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static int GetClosestColorRBG(this Color32 color, List<Color32> colors, Color32 target)
    {
        var colorDiffs = colors.Select(n => ColorDiff(n, target)).Min(n => n);
        return colors.FindIndex(n => ColorDiff(n, target) == colorDiffs);
    }

    public static bool Equals(this Color32 color1, Color32 color2)
    {
        return color1.r == color2.r && color1.g == color2.g && color1.b == color2.b;
    }
    public static float GetHue(this Color32 color)
    {
        float min;
        float max;
        float delta;
        float hue;
        min = Mathf.Min(color.r, color.g, color.b);
        max = Mathf.Max(color.r, color.g, color.b);
        delta = max - min;


        if (color.r == max)
            hue = (color.g - color.b) / delta;       // between yellow & magenta
        else if (color.g == max)
            hue = 2 + (color.b - color.r) / delta;   // between cyan & yellow
        else
            hue = 4 + (color.r - color.g) / delta;   // between magenta & cyan
        hue *= 60;               // degrees
        if (hue < 0)
            hue += 360;

        return hue / 360f;
    }
    public static float GetSaturation(this Color32 color)
    {
        float min;
        float max;
        float delta;
        min = Mathf.Min(color.r, color.g, color.b);
        max = Mathf.Max(color.r, color.g, color.b);
        delta = max - min;


        if (max != 0)
        {
            return delta / max;
        }
        else
        {
            return 0;
        }
    }
    public static float GetValue(this Color32 color)
    {
        float min;
        float max;
        float delta;
        min = Mathf.Min(color.r, color.g, color.b);
        max = Mathf.Max(color.r, color.g, color.b);
        delta = max - min;


        if (max != 0)
        {
            return delta / max;
        }
        else
        {
            return 0;
        }
    }






    // distance in RGB space
    private static int ColorDiff(Color32 c1, Color32 c2)
    {
        return (int)Mathf.Sqrt((c1.r - c2.r) * (c1.r - c2.r)
                             + (c1.g - c2.g) * (c1.g - c2.g)
                             + (c1.b - c2.b) * (c1.b - c2.b));
    }
}
