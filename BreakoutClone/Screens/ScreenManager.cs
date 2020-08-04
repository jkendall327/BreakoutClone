using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BreakoutClone.Screens
{
    public class ScreenManager
    {
        private GameScreen activeScreen;
        private StartScreen startScreen;
        private ActionScreen actionScreen;
        private OptionsScreen optionsScreen;
         
        private readonly GameComponentCollection Components;
        private readonly ContentManager Content;
        private readonly Breakout game;
        private readonly SpriteBatch spriteBatch;
         
        private KeyboardState keyboardState;
        private KeyboardState oldKeyboardState;
         
        private MouseState oldMouseState;

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

            /*
             * Switch on the run-time type of activeScreen.
             * Can't use a type directly in a switch statement,
             * so use _ to discard value immediately.
             * 
             * Execution then passed off to handler functions.
             * 
             * TODO: maybe a place for delegate trickery?
             */

            switch (activeScreen)
            {
                case ActionScreen _:
                    HandleActionScreenInput();
                    break;
                case OptionsScreen _:
                    HandleOptionsScreenInput();
                    break;
                case StartScreen _:
                    HandleStartScreenInput();
                    break;
                default:
                    Console.WriteLine("ERROR LMAO");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(activeScreen));
            }

            oldKeyboardState = keyboardState;
        }

        private void HandleActionScreenInput()
        {
            // Input relating to screens, not the actual game content.
            if (Helper.CheckKey(Keys.Escape, oldKeyboardState))
            {
                ChangeScreen(startScreen);
            }

            // Checking mouse and keyboard input.

            CheckMouse();

            // TODO: is there a way to pass the key value directly to entitymanager?

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                //actionScreen.EntitiesManager.HandleInput(Keys.Right);
                actionScreen.EntitiesManager.Player.MoveRight();
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                actionScreen.EntitiesManager.HandleInput(Keys.Left);
            }

            if (Helper.CheckKey(Keys.Space, oldKeyboardState))
            {
                actionScreen.EntitiesManager.HandleInput(Keys.Space);
            }
        }

        private void CheckMouse()
        {
            MouseState newMouseState = Mouse.GetState();

            if (oldMouseState.X != newMouseState.X)
            {
                if (Breakout.Viewport.Bounds.Contains(newMouseState.Position))
                {
                    actionScreen.EntitiesManager.HandleInput(newMouseState);
                }
            }

            // TODO: making an overload of HandleInput that accepts a bool is
            // very ugly. Way to improve this?
            if (newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                actionScreen.EntitiesManager.HandleInput(true);
            }

            oldMouseState = newMouseState;
        }

        /* 
         * TODO: Would be nice if you could click on menu options.
         * Make rectangles in the menuComponent class?
         */

        private void HandleOptionsScreenInput()
        {
            if (Helper.CheckKey(Keys.Escape, oldKeyboardState))
            {
                ChangeScreen(startScreen);
            }
        }

        private void HandleStartScreenInput()
        {
            if (Helper.CheckKey(Keys.Enter, oldKeyboardState))
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

            if (Helper.CheckKey(Keys.Escape, oldKeyboardState))
            {
                game.Exit();
            }
        }

    }
}
