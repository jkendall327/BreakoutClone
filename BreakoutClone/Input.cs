using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone
{
    public class Input
    {
        private GameScreen activeScreen;
        public Input()
        {

        }

        public void Update(GameScreen activeScreen)
        {
            this.activeScreen = activeScreen;

            if (activeScreen is StartScreen)
            {
                Console.WriteLine("starting screen");
            }
            if (activeScreen is OptionsScreen)
            {
                Console.WriteLine("options");
            }
            if (activeScreen is ActionScreen)
            {
                Console.WriteLine("gaming");
            }

            //if (activeScreen == startScreen)
            //{
            //    if (CheckKey(Keys.Enter))
            //    {
            //        if (startScreen.SelectedIndex == 0)
            //        {
            //            activeScreen.Hide();
            //            activeScreen = actionScreen;
            //            activeScreen.Show();
            //        }
            //        if (startScreen.SelectedIndex == 1)
            //        {
            //            activeScreen.Hide();
            //            activeScreen = optionsScreen;
            //            activeScreen.Show();
            //        }
            //        if (startScreen.SelectedIndex == 2)
            //        {
            //            this.Exit();
            //        }
            //    }
            //}
        }
    }
}
