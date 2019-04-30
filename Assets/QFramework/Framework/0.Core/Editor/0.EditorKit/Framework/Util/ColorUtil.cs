using UnityEngine;

namespace EGO.Framework.Util
{
    public static class ColorUtil
    {
        public static string ToText(this Color color)
        {
            return $"{color.r}@{color.g}@{color.b}@{color.a}";
        }

        public static Color ToColor(this string colorText)
        {
            var channels = colorText.Split('@');
            return new Color(
                float.Parse(channels[0]),
                float.Parse(channels[1]),
                float.Parse(channels[2]),
                float.Parse(channels[3]));
        }
    }
}