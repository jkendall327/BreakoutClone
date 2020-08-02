﻿using BreakoutClone.Content;
using BreakoutClone.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BreakoutClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Breakout : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        GameScreen activeScreen;
        StartScreen startScreen;
        ActionScreen actionScreen;
        OptionsScreen optionsScreen;

        ScreenManager screenManager;

        private Input input;

        public static Breakout Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }

        private readonly int gameWidth = 500;
        private readonly int gameHeight = 700;

        public Breakout()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = gameWidth,
                PreferredBackBufferHeight = gameHeight
            };

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.Load(Content);

            screenManager = new ScreenManager(this, spriteBatch);

            screenManager.LoadScreens();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            // Pause the game if window loses focus.
            if (IsActive == false)
            {
                return;
            }

            screenManager.CheckInput();

            base.Update(gameTime);

        }

        private void CheckInput()
        {
            keyboardState = Keyboard.GetState();

            if (activeScreen == startScreen)
            {
                if (CheckKey(Keys.Enter))
                {
                    if (startScreen.SelectedIndex == 0)
                    {
                        activeScreen.Hide();
                        activeScreen = actionScreen;
                        activeScreen.Show();
                    }
                    if (startScreen.SelectedIndex == 1)
                    {
                        activeScreen.Hide();
                        activeScreen = optionsScreen;
                        activeScreen.Show();
                    }
                    if (startScreen.SelectedIndex == 2)
                    {
                        this.Exit();
                    }
                }
            }

            if (activeScreen == optionsScreen)
            {
                if (CheckKey(Keys.Escape))
                {
                    activeScreen.Hide();
                    activeScreen = startScreen;
                    activeScreen.Show();
                }
            }

            if (activeScreen == actionScreen)
            {
                if (CheckKey(Keys.Escape))
                {
                    activeScreen.Hide();
                    activeScreen = optionsScreen;
                    activeScreen.Show();
                }
            }

            oldKeyboardState = keyboardState;
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            base.Draw(gameTime);

            spriteBatch.End();

        }
    }
}
