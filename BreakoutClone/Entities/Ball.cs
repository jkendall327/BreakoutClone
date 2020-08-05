using BreakoutClone.Content;
using BreakoutClone.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BreakoutClone
{
    class Ball : IDrawable
    {
        private readonly Random random = new Random();

        readonly Texture2D Image = Assets.Ball;

        public int Width { get; set; }

        public int Height { get; set; }

        public Vector2 Position;

        Vector2 StartingPosition;

        private double xvelocity;
        public double XVelocity
        {
            get { return xvelocity; }
            set { xvelocity = Helper.Clamp(value, -10, 10); }
        }

        private double yvelocity;

        public double YVelocity
        {
            get { return yvelocity; }
            set { yvelocity = Helper.Clamp(value, -10, 10); }
        }

        public Rectangle PaddleHitbox { get; set; }

        public bool IsActive { get; set; }

        public Ball(Vector2 position, float XVelocity, float YVelocity)
        {
            Position = position;
            StartingPosition = Position;

            this.XVelocity = XVelocity;
            this.YVelocity = YVelocity;

            Width = Image.Width;
            Height = Image.Height;
        }

        public void Subscribe(Paddle paddle)
        {
            paddle.PaddleMoved += OnPaddleMoved;
        }

        private void OnPaddleMoved(object sender, Rectangle hitbox)
        {
            PaddleHitbox = hitbox;
        }

        public void Update(Wall wall, List<Item> items)
        {
            if (IsActive == false)
            {
                return;
            }

            ChangePosition();

            CheckForWalls();

            CheckForPaddle();

            CheckForBrick(wall);

            CheckForItem(items);

        }

        private void ChangePosition()
        {
            Position.X += (float)XVelocity;
            Position.Y += (float)YVelocity;
        }

        private void CheckForWalls()
        {

            // Velocity multiplied by -0.9 to slow down ball on wall hit. 

            Double WallSpeedModifier = -0.9;

            // Hit left wall
            if (Position.X < 0)
            {
                Position.X = Helper.Clamp(Position.X, 0, Breakout.ScreenSize.X - Width);
                XVelocity *= WallSpeedModifier;
            }

            // Hit right wall
            if (Position.X + Width > Breakout.ScreenSize.X)
            {
                Position.X = Helper.Clamp(Position.X, 0, Breakout.ScreenSize.X - Width);
                XVelocity *= WallSpeedModifier;
            }

            // Hit top of screen

            if (Position.Y < 0)
            {
                Position.Y = Helper.Clamp(Position.Y, 0, Breakout.ScreenSize.Y);
                YVelocity *= WallSpeedModifier;
            }

            // Hit bottom of screen
            if (Position.Y + Height > Breakout.ScreenSize.Y)
            {
                Reset();
            }
        }

        private int GetOffset(Rectangle rectangle)
        {
            float PointOfContactOnPaddle = rectangle.X + rectangle.Width - Position.X + Width / 2;

            int offset = Convert.ToInt32((PaddleHitbox.Width - PointOfContactOnPaddle)) / 5;

            return Helper.Clamp(offset, 0, int.MaxValue);
        }

        private void CheckForPaddle()
        {
            Rectangle ballHitbox = GetCurrentHitbox();

            if (ballHitbox.Intersects(PaddleHitbox) == false)
            {
                return;
            }

            int offset = GetOffset(PaddleHitbox);

            // TODO: magic number

            XVelocity = offset - 6;

            YVelocity *= -1;
        }

        private void CheckForBrick(Wall wall)
        {
            Rectangle ballHitbox = GetCurrentHitbox();

            foreach (Brick brick in wall.BrickWall)
            {
                if (brick.IsAlive && ballHitbox.Intersects(brick.Hitbox))
                {
                    brick.IsAlive = false;

                    wall.BricksLeft -= 1;
                    Console.WriteLine(wall.BricksLeft);

                    // Invert direction and increase speed.
                    YVelocity *= -1.3;

                    // Breaking out of the loop is what stops the ball
                    // from destroying multiple bricks at once.
                    // TODO: idea for a powerup?
                    break;
                }
            }
        }

        private void CheckForItem(List<Item> items)
        {
            Rectangle ballHitbox = GetCurrentHitbox();

            foreach (Item item in items)
            {
                if (item.Hitbox.Intersects(ballHitbox))
                {
                    item.IsVisible = false;
                    item.Activate();
                }

            }

        }

        private Rectangle GetCurrentHitbox()
        {
            return new Rectangle(Position.ToPoint(), new Point(Width, Height));
        }

        public void Reset()
        {
            IsActive = false;
            Position = StartingPosition;
        }

        private double RandomiseLaunchDirection()
        {
            double velocity;

            if (random.Next() % 2 == 0)
            {
                velocity = 3;
            }
            else
            {
                velocity = -3;
            }

            return velocity;
        }

        public void Launch()
        {
            IsActive = true;

            XVelocity = RandomiseLaunchDirection();
            YVelocity = RandomiseLaunchDirection();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
