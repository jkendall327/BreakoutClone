namespace BreakoutClone
{
    using System;
    using System.Collections.Generic;
    using BreakoutClone.Content;
    using BreakoutClone.Entities;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    internal class Ball : IDrawable
    {
        private readonly Random random = new Random();

        private readonly Texture2D image = Assets.Ball;

        public int Width { get; set; }

        public int Height { get; set; }

        public Vector2 Position;

        private Vector2 startingPosition;

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

        public bool IsActive { get; set; }

        public Ball(Vector2 position, float xVelocity, float yVelocity)
        {
            this.Position = position;
            this.startingPosition = this.Position;

            this.XVelocity = xVelocity;
            this.YVelocity = yVelocity;

            this.Width = this.image.Width;
            this.Height = this.image.Height;
        }

        public void Update(Wall wall, List<Item> items, Paddle paddle)
        {
            if (IsActive == false)
            {
                return;
            }

            ChangePosition();

            CheckForWalls();

            CheckForPaddle(paddle);

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
            // Velocity multiplied by -0.9 to slow down ball on hit. 

            Double WallSpeedModifier = -0.9;

            XVelocity = BounceOffSideWalls(WallSpeedModifier, Position.X, xvelocity);

            YVelocity = BounceOffTopWall(WallSpeedModifier, Position.Y, YVelocity);

            CheckForBottomWall(Position.Y);

            ClampOnScreen();
        }

        private double BounceOffTopWall(double WallSpeedModifier, float y, double velocity)
        {
            // Hit top of screen

            if (y < 0)
            {
                return velocity *= WallSpeedModifier;
            }

            return velocity;
        }

        private void CheckForBottomWall(float y)
        {
            if (y + Height > Breakout.ScreenSize.Y)
            {
                Reset();
            }
        }

        private double BounceOffSideWalls(double WallSpeedModifier, float x, double velocity)
        {
            // If ball's x-coordinate is off the screen, make it bounce against the wall.

            if (x != Helper.Clamp(x, 0, Breakout.ScreenSize.X - Width))
            {
                return velocity *= WallSpeedModifier;
            }

            return velocity;
        }

        private void ClampOnScreen()
        {
            Position.X = Helper.Clamp(Position.X, 0, Breakout.ScreenSize.X - Width);
            Position.Y = Helper.Clamp(Position.Y, 0, Breakout.ScreenSize.Y);
        }

        private int GetOffset(Rectangle rectangle)
        {
            float PointOfContactOnPaddle = rectangle.X + rectangle.Width - Position.X + Width / 2;

            int offset = Convert.ToInt32((rectangle.Width - PointOfContactOnPaddle)) / 5;

            return Helper.Clamp(offset, 0, int.MaxValue);
        }

        private void CheckForPaddle(Paddle paddle)
        {
            Rectangle paddleHitbox = new Rectangle(paddle.X, paddle.Y, paddle.Width, paddle.Height);

            Rectangle ballHitbox = GetCurrentHitbox();

            if (ballHitbox.Intersects(paddleHitbox) == false)
            {
                return;
            }

            int offset = GetOffset(paddleHitbox);

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
            Position = startingPosition;
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
            spritebatch.Draw(image, Position, Color.White);
        }

    }
}
