using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        public static int Value(this Dir dir)
        {
            switch (dir)
            {
                case Dir.Positive:
                    return 1;
                case Dir.Negative:
                    return -1;
                case Dir.None:
                    return 0;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), "Invalid Dir value");
            }
        }
    }

    /// <summary>
    /// 用于区分正负方向的枚举序列，用于如"上一关，下一关"的枚举项
    /// </summary>
    public enum Dir
    {
        Positive,
        Negative,
        None
    }
}

