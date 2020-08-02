using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone.Screens
{
    public class ScreenManager
    {
        GameScreen activeScreen;
        StartScreen startScreen;
        ActionScreen actionScreen;
        OptionsScreen optionsScreen;

        GameComponentCollection Components;
        ContentManager Content;
        Breakout game;
        SpriteBatch spriteBatch;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;


        public ScreenManager(Breakout game, SpriteBatch spriteBatch)
        {
            this.game = game;
            Components = game.Components;
            Content = game.Content;
            this.spriteBatch = spriteBatch;
        }

        public void LoadScreens()
        {
            startScreen = new StartScreen(game, spriteBatch, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("background"));
            Components.Add(startScreen);
            startScreen.Hide();

            actionScreen = new ActionScreen(game, spriteBatch, Content.Load<Texture2D>("background"));
            Components.Add(actionScreen);
            actionScreen.Hide();

            optionsScreen = new OptionsScreen(game, spriteBatch, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("background"));
            Components.Add(optionsScreen);
            optionsScreen.Hide();

            activeScreen = startScreen;
            activeScreen.Show();
        }

        private void ChangeScreen(GameScreen screen)
        {
            activeScreen.Hide();
            activeScreen = screen;
            activeScreen.Show();
        }

        public void CheckInput()
        {
            keyboardState = Keyboard.GetState();

            if (activeScreen == startScreen)
            {
                if (CheckKey(Keys.Enter))
                {
                    if (startScreen.SelectedIndex == 0)
                    {
                        ChangeScreen(actionScreen);
                    }
                    if (startScreen.SelectedIndex == 1)
                    {
                        ChangeScreen(optionsScreen);
                    }
                    if (startScreen.SelectedIndex == 2)
                    {
                        game.Exit();
                    }
                }
            }

            if (activeScreen == optionsScreen)
            {
                if (CheckKey(Keys.Escape))
                {
                    ChangeScreen(startScreen);

                }
            }

            if (activeScreen == actionScreen)
            {
                if (CheckKey(Keys.Escape))
                {
                    ChangeScreen(optionsScreen);
                }
            }

            oldKeyboardState = keyboardState;
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }

    }
}
