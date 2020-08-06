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
        private PauseScreen pauseScreen;
         
        private readonly GameComponentCollection Components;
        private readonly ContentManager Content;
        private readonly Breakout game;
        private readonly SpriteBatch spriteBatch;
         
        private KeyboardState keyboardState;
        private KeyboardState oldKeyboardState;
         
        private MouseState oldMouseState;

        private InputHandler inputHandler;

        public ScreenManager(Breakout game, SpriteBatch spriteBatch)
        {
            this.game = game;
            Components = game.Components;
            Content = game.Content;
            this.spriteBatch = spriteBatch;
        }

        public void LoadScreens()
        {
            inputHandler = new InputHandler();

            startScreen = new StartScreen(game, spriteBatch, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("background"));
            Components.Add(startScreen);
            startScreen.Hide();

            actionScreen = new ActionScreen(game, spriteBatch, Content.Load<Texture2D>("background"));
            Components.Add(actionScreen);
            actionScreen.Hide();

            optionsScreen = new OptionsScreen(game, spriteBatch, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("background"));
            Components.Add(optionsScreen);
            optionsScreen.Hide();

            pauseScreen = new PauseScreen(game, spriteBatch, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("background"));
            Components.Add(pauseScreen);
            pauseScreen.Hide();

            activeScreen = startScreen;
            inputHandler.keyPressed += activeScreen.OnKeyPressed;
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
            inputHandler.Update();

            keyboardState = Keyboard.GetState();

            //Switch on the run-time type of activeScreen using pattern-matching.

            switch (activeScreen)
            {
                case ActionScreen _:
                    HandleInput(activeScreen as ActionScreen);
                    break;
                case OptionsScreen _:
                    HandleInput(activeScreen as OptionsScreen);
                    break;
                case StartScreen _:
                    HandleInput(activeScreen as StartScreen);
                    break;
                case PauseScreen _:
                    HandleInput(activeScreen as PauseScreen);
                    break;
                default:
                    Console.WriteLine("ERROR LMAO");
                    break;
                case null:
                    throw new ArgumentNullException(nameof(activeScreen));
            }

            oldKeyboardState = keyboardState;
        }

        private void HandleInput(ActionScreen actionScreen)
        {
            // Checking mouse and keyboard input.

            CheckMouse();

            // TODO: is there a way to pass the key value directly to entitymanager?

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                actionScreen.EntitiesManager.Player.MoveRight();
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                actionScreen.EntitiesManager.Player.MoveLeft();
            }

            if (Helper.CheckKey(Keys.Space, oldKeyboardState))
            {
                actionScreen.EntitiesManager.HandleInput(Keys.Space);
            }

            HandleEsc();
        }

        private void HandleInput(OptionsScreen optionsScreen)
        {
            if (Helper.CheckKey(Keys.Escape, oldKeyboardState))
            {
                HandleEsc();
            }
        }
        private void HandleInput(StartScreen startScreen)
        {
            if (Helper.CheckKey(Keys.Enter, oldKeyboardState))
            {
                switch (startScreen.SelectedIndex)
                {
                    case 0:
                        // Make a new version of actionScreen to set the gamestate clean.
                        ResetGame();
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

            HandleEsc();
        }

        private void HandleInput(PauseScreen pauseScreen)
        {
            if (Helper.CheckKey(Keys.Enter, oldKeyboardState))
            {
                switch (pauseScreen.SelectedIndex)
                {
                    case 0:
                        ChangeScreen(actionScreen);
                        break;
                    case 1:
                        ChangeScreen(startScreen);
                        break;
                    default:
                        break;
                }
            }

            HandleEsc();
        }

        private void HandleEsc()
        {
            bool wasEscPressed = Helper.CheckKey(Keys.Escape, oldKeyboardState);

            if (wasEscPressed == false)
            {
                return;
            }

            if (activeScreen is ActionScreen)
            {
                ChangeScreen(pauseScreen);
                return;
            }

            if (activeScreen is StartScreen)
            {
                game.Exit();
            }

            else
            {
                ChangeScreen(startScreen);
            }
        }

        private void CheckMouse()
        {
            MouseState newMouseState = Mouse.GetState();

            if (oldMouseState.X != newMouseState.X && Breakout.Viewport.Bounds.Contains(newMouseState.Position))
            {
                actionScreen.EntitiesManager.HandleInput(newMouseState);
            }

            // TODO: making an overload of HandleInput that accepts a bool is
            // very ugly. Way to improve this?
            if (newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
            {
                actionScreen.EntitiesManager.HandleInput(true);
            }

            oldMouseState = newMouseState;
        }

        private void ResetGame()
        {
            Components.Remove(actionScreen);
            actionScreen = new ActionScreen(game, spriteBatch, Content.Load<Texture2D>("background"));
            Components.Add(actionScreen);
            ChangeScreen(actionScreen);
        }
    }
}
