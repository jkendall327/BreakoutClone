using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BreakoutClone
{
    class Paddle : IDrawable, IUpdate
    {
        private int width;

        public int Width
        {
            get { return width; }
            set { if (value > 0) { width = value; } }
        }

        private int height;

        public int Height
        {
            get { return height; }
            set { if (value > 0) { height = value; } }
        }

        Vector2 Position;

        private const int PixelsToMovePaddleBy = 5;

        public event EventHandler<Rectangle> PaddleMoved;

        public Paddle(Vector2 position, int width, int height)
        {
            Position = position;
            Width = width;
            Height = height;
        }

        public void Update()
        {
        }

        private float ClampPaddleLocation(float x)
        {
            return Helper.Clamp(x, 0, Breakout.ScreenSize.X - Width);
        }

        public void MoveTo(float xCoordinate)
        {
            Position.X = ClampPaddleLocation(xCoordinate);

            PaddleMoved.Invoke(this, new Rectangle(Position.ToPoint(), new Point(Width, Height)));
        }

        public void MoveLeft()
        {
            Position.X -= PixelsToMovePaddleBy;

            Position.X = ClampPaddleLocation(Position.X);

            PaddleMoved.Invoke(this, new Rectangle(Position.ToPoint(), new Point(Width, Height)));
        }

        public void MoveRight()
        {
            Position.X += PixelsToMovePaddleBy;

            Position.X = ClampPaddleLocation(Position.X);

            PaddleMoved.Invoke(this, new Rectangle(Position.ToPoint(), new Point(Width, Height)));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws the paddle based on the current hitbox using primitives.

            var Hitbox = new Rectangle(Position.ToPoint(), new Point(Width, Height));

            Color[] data = new Color[Hitbox.Width * Hitbox.Height];
            Texture2D rectTexture = new Texture2D(Breakout.Instance.GraphicsDevice, Hitbox.Width, Hitbox.Height);

            for (int i = 0; i < data.Length; ++i)
                data[i] = Color.White;

            rectTexture.SetData(data);
            var position = new Vector2(Hitbox.Left, Hitbox.Top);

            spriteBatch.Draw(rectTexture, position, Color.White);

        }
    }
}
