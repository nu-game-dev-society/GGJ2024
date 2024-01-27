using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class MathExtensions
    {
        public static int ClampWrapped(int value, int min, int max)
        {
            int newV = Mathf.Clamp(value, min, max);
            if (newV == value)
                return value;

            return min + (value - newV - 1);
        }
    }

}
