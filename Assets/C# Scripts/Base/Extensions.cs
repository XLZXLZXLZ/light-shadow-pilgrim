using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions
{
    public static class Extensions
    {
        public static Color GetTransparent(this Color color)
        {
            var alpha = color.a;
            return color - new Color(0, 0, 0, alpha);
        }
    }
}
