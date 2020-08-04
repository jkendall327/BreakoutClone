using Microsoft.Xna.Framework.Input;
using System;

namespace BreakoutClone
{
    public static class Helper
    {
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        public static bool CheckKey(Keys theKey, KeyboardState oldKeyboardState)
        {
            return Keyboard.GetState().IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }
    }
}
