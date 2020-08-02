using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BreakoutClone
{
    class Ball : IDrawable
    {
        readonly Texture2D Image = Assets.Ball;

        public int Width { get; set; }

        public int Height { get; set; }

        Vector2 Position;

        Vector2 OriginalPosition = new Vector2(Breakout.ScreenSize.X / 2, Breakout.ScreenSize.Y / 2);

        // TODO: Clamp the velocities here in the set functions, it's what they're for! 
        public double XVelocity { get; set; }

        public double YVelocity { get; set; }

        public Rectangle PaddleHitbox { get; set; }

        public bool IsActive { get; set; }

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
            if (IsActive)
            {
                Move(wall);
            }
            else
            {
                CheckForLaunch();
            }

        }

        private void Move(Wall wall)
        {
            XVelocity = Helper.Clamp(XVelocity, -10, 10);
            YVelocity = Helper.Clamp(YVelocity, -10, 10);

            ChangePosition();

            CheckForWalls();

            // Some basic checks so it's not checking for collision literally every frame.
            // Bottom half of screen, check for paddle. Top half, check for bricks.

            if (Position.Y > 200)
            {
                CheckForPaddle();
            }

            if (Position.Y < 200)
            {
                CheckForBrick(wall);
            }
        }

        public void Reset()
        {
            IsActive = false;
            Position = OriginalPosition;
        }

        private void CheckForLaunch()
        {
            MouseState newMouseState = Mouse.GetState();

            // LMB was down and is now released, i.e. a left click.
            bool MouseClick = newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed;

            if (MouseClick)
            {
                Launch();
            }

            oldMouseState = newMouseState;
        }

        public void Launch()
        {
            IsActive = true;

            bool newXDirection = new Random().Next() % 2 == 0;
            bool newYDirection = new Random().Next() % 2 == 0;

            if (newXDirection)
            {
                XVelocity = 3;
            }
            else
            {
                XVelocity = -3;
            }

            if (newYDirection)
            {
                YVelocity = 3;
            }
            else
            {
                YVelocity = -3;
            }

        }

        private void ChangePosition()
        {
            Position.X += (float)XVelocity;
            Position.Y += (float)YVelocity;
        }

        private void CheckForBrick(Wall wall)
        {
            Rectangle ballHitbox = new Rectangle(Position.ToPoint(), new Point(Width, Height));

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

        private void CheckForPaddle()
        {
            Rectangle ballHitbox = new Rectangle(Position.ToPoint(), new Point(Width, Height));

            if (ballHitbox.Intersects(PaddleHitbox))
            {
                float PointOfContactOnPaddle = PaddleHitbox.X + PaddleHitbox.Width - Position.X + Width / 2;

                int offset = Convert.ToInt32((PaddleHitbox.Width - PointOfContactOnPaddle));
                offset /= 5;

                if (offset < 0)
                {
                    offset = 0;
                }

                switch (offset)
                {
                    case 0:
                        XVelocity = -6;
                        break;
                    case 1:
                        XVelocity = -5;
                        break;
                    case 2:
                        XVelocity = -4;
                        break;
                    case 3:
                        XVelocity = -3;
                        break;
                    case 4:
                        XVelocity = -2;
                        break;
                    case 5:
                        XVelocity = -1;
                        break;
                    case 6:
                        XVelocity = 1;
                        break;
                    case 7:
                        XVelocity = 2;
                        break;
                    case 8:
                        XVelocity = 3;
                        break;
                    case 9:
                        XVelocity = 4;
                        break;
                    case 10:
                        XVelocity = 5;
                        break;
                    default:
                        XVelocity = 6;
                        break;
                }

                YVelocity *= -1;
                Position.Y = PaddleHitbox.Y - Height + 1;
            }
        }

        private void CheckForWalls()
        {
            // Velocity multiplied by -0.9 to slow down ball on wall hit. 

            Double WallSpeedModifier = -0.9;

            // Hit left wall
            if (Position.X < 0)
            {
                Position.X = 0;
                XVelocity *= WallSpeedModifier;
            }

            // Hit right wall
            if (Position.X + Width > Breakout.ScreenSize.X)
            {
                Position.X = Breakout.ScreenSize.X - Width;
                XVelocity *= WallSpeedModifier;
            }

            // Hit top of screen

            if (Position.Y < 0)
            {
                Position.Y = 0;
                YVelocity *= WallSpeedModifier;
            }

            // Hit bottom of screen
            if (Position.Y + Height > Breakout.ScreenSize.Y)
            {
                Position.X = Breakout.ScreenSize.X / 2;
                Position.Y = Breakout.ScreenSize.Y / 2;
                IsActive = false;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
