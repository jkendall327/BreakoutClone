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
            
            //TODO: next task is drawing the paddle dynamically so the size can be changed mid-game.

            switch (activeScreen)
            {
                case ActionScreen a:
                    if (CheckKey(Keys.Escape))
                    {
                        ChangeScreen(startScreen);
                    }
                    break;
                case OptionsScreen o:
                    if (CheckKey(Keys.Escape))
                    {
                        ChangeScreen(startScreen);
                    }
                    break;
                case StartScreen s:
                    if (CheckKey(Keys.Enter))
                    {
                        switch (startScreen.SelectedIndex)
                        {
                            case 0:
                                ChangeScreen(actionScreen);
                                break;
                            case 1:
                                ChangeScreen(optionsScreen);
                                break;
                            case 2:
                                game.Exit();
                                break;
                            default:
                                break;
                        }

                    }
                    break;
                default:
                    Console.WriteLine("ERROR LMAO");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(activeScreen));
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
