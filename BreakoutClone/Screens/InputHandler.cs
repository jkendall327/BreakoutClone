using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone.Screens
{
    class InputHandler
    {
        private KeyboardState oldKeyboardState;
        private KeyboardState keyboardState;

        public event EventHandler<KeyboardEventArgs> keyPressed;

        public class KeyboardEventArgs
        {
            List<Keys> pressedKeys = new List<Keys>();

            public KeyboardEventArgs(List<Keys> keys)
            {
                pressedKeys = keys;
            }
        }

        public InputHandler()
        {

        }

        public void Update()
        {
            CheckKeyboard();
        }

        private void CheckKeyboard()
        {
            keyboardState = Keyboard.GetState();

            // No keys were pressed this frame or last frame, meaning there can be no button presses.

            if (keyboardState.GetPressedKeys().Length == 0 && oldKeyboardState == keyboardState)
            {
                return;
            }

            /*
             * We want not the keys pressed on this frame, but 
             * those pressed last frame and released this frame. 
             * Therefore we have to filter.
             */

            Keys[] pressedKeys = oldKeyboardState.GetPressedKeys();
            List<Keys> keysReleasedThisFrame = new List<Keys>();

            foreach (Keys key in pressedKeys)
            {
                if (Helper.CheckKey(key, oldKeyboardState))
                {
                    keysReleasedThisFrame.Add(key);
                }
            }

            keyPressed.Invoke(this, new KeyboardEventArgs(keysReleasedThisFrame));

            oldKeyboardState = keyboardState;
        }
    }
}
