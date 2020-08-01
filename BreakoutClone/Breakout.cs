using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BreakoutClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Breakout : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Breakout Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }

        private readonly int gameWidth = 500;
        private readonly int gameHeight = 700;

        EntityManager entityManager;

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
            entityManager = new EntityManager();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.Load(Content);

            entityManager.CreateEntities();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Pause the game if window loses focus.
            if (IsActive == false)
            {
                return;
            }

            entityManager.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            entityManager.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
