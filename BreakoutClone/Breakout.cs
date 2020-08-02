using BreakoutClone.Content;
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

            startScreen = new StartScreen(this, spriteBatch, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("background"));
            Components.Add(startScreen);
            startScreen.Hide();

            actionScreen = new ActionScreen(this, spriteBatch, Content.Load<Texture2D>("background"));
            Components.Add(actionScreen);
            actionScreen.Hide();

            optionsScreen = new OptionsScreen(this, spriteBatch, Content.Load<SpriteFont>("menufont"), Content.Load<Texture2D>("background"));
            Components.Add(optionsScreen);
            optionsScreen.Hide();

            activeScreen = startScreen;
            activeScreen.Show();

            input = new Input();
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

            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            input.Update(activeScreen);

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

            base.Update(gameTime);
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
