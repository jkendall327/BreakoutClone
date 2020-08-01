using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone
{
    class Ball : IDrawable, IUpdate
    {
        Texture2D Image = Assets.Ball;

        public int Width { get; set; }

        public int Height { get; set; }

        Vector2 Position;

        public float XVelocity { get; set; }

        public float YVelocity { get; set; }

        public Rectangle PaddleHitbox { get; set; }

        public Ball(Vector2 position, float XVelocity, float YVelocity)
        {
            Position = position;

            this.XVelocity = XVelocity;
            this.YVelocity = YVelocity;

            Width = Image.Width;
            Height = Image.Height;
        }

        // Subscribes to paddle so it can update its version
        // of the player's hitbox whenever it moves.
        public void Subscribe(Paddle paddle)
        {
            paddle.paddleMoved += OnPaddleMoved;
        }

        private void OnPaddleMoved(object sender, Rectangle hitbox)
        {
            PaddleHitbox = hitbox;
        }

        public void Update()
        {
            Move();
        }

        private void Move()
        {
            Position.X += XVelocity;
            Position.Y += YVelocity;

            CheckForWalls();

            CheckForPaddle();
        }

        private void CheckForPaddle()
        {
            Rectangle ballHitbox = new Rectangle(Position.ToPoint(), new Point(Width, Height));

            // If the two hitboxes overlap.
            if (Rectangle.Intersect(PaddleHitbox, ballHitbox) != Rectangle.Empty)
            {
                XVelocity *= -1;
                YVelocity *= -1;
                Position.Y = PaddleHitbox.Top - Height;

            }
        }

        private void CheckForWalls()
        {
            // Hit left wall
            if (Position.X < 0)
            {
                Position.X = 0;
                XVelocity *= -1;
            }

            // Hit right wall
            if (Position.X + Width > Breakout.ScreenSize.X)
            {
                Position.X = Breakout.ScreenSize.X - Width;
                XVelocity *= -1;
            }

            // Hit top of screen

            if (Position.Y < 0)
            {
                Position.Y = 0;
                YVelocity *= -1;
            }

            // Hit bottom of screen
            if (Position.Y + Height > Breakout.ScreenSize.Y)
            {
                Position.Y = Breakout.ScreenSize.Y - Height;
                YVelocity *= -1;
            }

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
