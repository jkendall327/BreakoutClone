using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BreakoutClone
{
    //TODO: next task is drawing the paddle dynamically so the size can be changed mid-game.

    class Paddle : IDrawable, IUpdate
    {
        readonly Texture2D Image;

        private int width;

        public int Width
        {
            get { return width; }
            set {
                if (value > 0)
                {
                width = value; // Set width to value
                UpdateHitbox();}; //Update hitbox with new width
            }
        }

        public int Height { get; set; }

        Rectangle Hitbox { get; set; }

        Vector2 Position;

        public event EventHandler<Rectangle> PaddleMoved;

        private void UpdateHitbox()
        {
            Hitbox = new Rectangle(Position.ToPoint(), new Point(Width, Height));
        }

        public Paddle(Vector2 position)
        {
            Position = position;

            Image = Assets.Paddle;

            Width = Image.Width;

            Height = Image.Height;

            Hitbox = Image.Bounds;
        }

        public void Update()
        {
        }

        public void MoveTo(float xCoordinate)
        {
            // Clamp on screen.
            if (xCoordinate + Width > Breakout.ScreenSize.X)
            {
                Position.X = Breakout.ScreenSize.X - Width;
            }
            else
            {
                Position.X = xCoordinate;
            }

            Hitbox = new Rectangle(Position.ToPoint(), new Point(Width, Height));

            PaddleMoved.Invoke(this, Hitbox);
        }

        public void MoveLeft()
        {
            Position.X -= 5;

            if (Position.X < 1)
            {
                Position.X = 1;
            }

            Hitbox = new Rectangle(Position.ToPoint(), new Point(Width, Height));

            PaddleMoved.Invoke(this, Hitbox);
        }

        public void MoveRight()
        {
            Position.X += 5;

            if (Position.X + Width > Breakout.ScreenSize.X)
            {
                Position.X = Breakout.ScreenSize.X - Width;
            }

            Hitbox = new Rectangle(Position.ToPoint(), new Point(Width, Height));

            PaddleMoved.Invoke(this, Hitbox);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws the paddle based on the hitbox using primitives.

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
