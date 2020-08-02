﻿using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BreakoutClone
{
    class Ball : IDrawable
    {
        readonly Texture2D Image = Assets.Ball;

        public int Width { get; set; }

        public int Height { get; set; }

        Vector2 Position;

        public double XVelocity { get; set; }

        public double YVelocity { get; set; }

        public Rectangle PaddleHitbox { get; set; }

        private readonly Random random = new Random();

        bool isActive = true;

        private MouseState oldMouseState;

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
            paddle.PaddleMoved += OnPaddleMoved;
        }

        private void OnPaddleMoved(object sender, Rectangle hitbox)
        {
            PaddleHitbox = hitbox;
        }

        public void Update(Wall wall)
        {
            MouseState newMouseState = Mouse.GetState();
            //process left-click
            if (newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed && oldMouseState.X == newMouseState.X && oldMouseState.Y == newMouseState.Y && isActive == false)
            {
                Launch();
            }

            if (isActive)
            {
                Position.X += (float)XVelocity;
                Position.Y += (float)YVelocity;

                CheckForWalls();

                // Some basic checks so it's not checking for collision literally every frame.
                // Bototm half of screen, check for paddle.
                // Top half, check for bricks.

                if (Position.Y > 200)
                {
                    CheckForPaddle();

                }

                if (Position.Y < 200)
                {
                    CheckForBrick(wall);
                } 
            }

            oldMouseState = newMouseState;
        }

        public void Launch()
        {
            isActive = true;

            // TODO: both could return zero, i.e. stationary ball.
            XVelocity = random.Next(-1, 1);
            YVelocity = random.Next(-1, 1);
        }

        private void CheckForBrick(Wall wall)
        {
            Rectangle ballHitbox = new Rectangle(Position.ToPoint(), new Point(Width, Height));

            foreach (Brick brick in wall.BrickWall)
            {
                if (brick.IsAlive && ballHitbox.Intersects(brick.Hitbox))
                {
                    brick.IsAlive = false;

                    // Invert direction and increase speed.
                    YVelocity *= -1.3;

                    // Breaking out of the loop is what stops the ball
                    // from destroying multiple bricks at once.
                    // TODO: idea for a powerup?
                    break;
                }
            }
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
            // Velocity multiplied by -0.9 to slow down ball
            // on wall hit. 
            // TODO: ball could hit zero velocity.

            // Hit left wall
            if (Position.X < 0)
            {
                Position.X = 0;
                XVelocity *= -0.9;
            }

            // Hit right wall
            if (Position.X + Width > Breakout.ScreenSize.X)
            {
                Position.X = Breakout.ScreenSize.X - Width;
                XVelocity *= -0.9;
            }

            // Hit top of screen

            if (Position.Y < 0)
            {
                Position.Y = 0;
                YVelocity *= -0.9;
            }

            // Hit bottom of screen
            if (Position.Y + Height > Breakout.ScreenSize.Y)
            {
                Position.X = Breakout.ScreenSize.X / 2;
                Position.Y = Breakout.ScreenSize.Y / 2;
                isActive = false;
            }


            //if (Position.Y + Height > Breakout.ScreenSize.Y)
            //{
            //    Position.Y = Breakout.ScreenSize.Y - Height;
            //    YVelocity *= -1;
            //}

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
