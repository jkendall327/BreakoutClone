using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone
{
    public class Input
    {
        //private KeyboardState oldKeyboardState;
        //private KeyboardState newKeyboardState;

        //private GameScreen activeScreen;
        //public Input()
        //{

        //}

        //private bool CheckKey(Keys theKey)
        //{
        //    return newKeyboardState.IsKeyUp(theKey) &&
        //        oldKeyboardState.IsKeyDown(theKey);
        //}

        //public void Update(GameScreen activeScreen)
        //{
        //    newKeyboardState = Keyboard.GetState();

        //    this.activeScreen = activeScreen;

        //    if (activeScreen is StartScreen)
        //    {
        //        Console.WriteLine("starting screen");
        //    }

        //    if (activeScreen is OptionsScreen)
        //    {
        //        if (CheckKey(Keys.Escape))
        //        {
        //            activeScreen.Hide();
        //            activeScreen = startScreen;
        //            activeScreen.Show();
        //        }
        //        Console.WriteLine("options");
        //    }

        //    if (activeScreen is ActionScreen)
        //    {
        //        Console.WriteLine("gaming");
        //    }


        //    oldKeyboardState = newKeyboardState;
        //}
    }
}
