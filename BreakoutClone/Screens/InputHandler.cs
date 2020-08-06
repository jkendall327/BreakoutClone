﻿using Microsoft.Xna.Framework.Input;
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
            Keys[] keys;

            public KeyboardEventArgs(Keys[] keys)
            {
                this.keys = keys;
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

            keyPressed.Invoke(this, new KeyboardEventArgs(keyboardState.GetPressedKeys()));

            oldKeyboardState = keyboardState;
        }
    }
}
