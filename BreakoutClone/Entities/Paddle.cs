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

        public void MoveTo(float xCoordinate)
        {
            Position.X = Helper.Clamp(xCoordinate, 0, Breakout.ScreenSize.X - Width);

            PaddleMoved.Invoke(this, new Rectangle(Position.ToPoint(), new Point(Width, Height)));
        }

        public void MoveLeft()
        {
            Position.X -= PixelsToMovePaddleBy;

            if (Position.X < 1)
            {
                Position.X = 1;
            }

            PaddleMoved.Invoke(this, new Rectangle(Position.ToPoint(), new Point(Width, Height)));
        }

        public void MoveRight()
        {
            Position.X += PixelsToMovePaddleBy;

            if (Position.X + Width > Breakout.ScreenSize.X)
            {
                Position.X = Breakout.ScreenSize.X - Width;
            }

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
