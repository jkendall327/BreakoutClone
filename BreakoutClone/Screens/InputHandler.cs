using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone.Screens
{
    public class KeyboardEventArgs
    {
        public readonly List<Keys> EventKeys = new List<Keys>();

        public KeyboardEventArgs(List<Keys> keys)
        {
            EventKeys = keys;
        }
    }

    class InputHandler
    {
        private KeyboardState oldKeyboardState;
        private KeyboardState keyboardState;

        public event EventHandler<KeyboardEventArgs> keyPressed;
        public event EventHandler<KeyboardEventArgs> keyHeld;



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

            GetKeysReleasedThisFrame();

            GetHeldKeys();

            oldKeyboardState = keyboardState;
        }

        private void GetHeldKeys()
        {
            /*
             * If a key was held last frame and this frame,
             * that means it's being held down.
             */

            if (oldKeyboardState.GetPressedKeys() == keyboardState.GetPressedKeys())
            {
                keyHeld.Invoke(this, new KeyboardEventArgs(keyboardState.GetPressedKeys().ToList()));
            }
        }

        private void GetKeysReleasedThisFrame()
        {
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

            keyPressed?.Invoke(this, new KeyboardEventArgs(keysReleasedThisFrame));
        }
    }
}
