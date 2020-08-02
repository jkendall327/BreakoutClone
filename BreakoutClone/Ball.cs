using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

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

        bool isActive = true;

        private MouseState oldMouseState;
        private KeyboardState oldKeyboardState;

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
            if (isActive)
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
            ClampSpeed();

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

        private void CheckForLaunch()
        {
            MouseState newMouseState = Mouse.GetState();
            KeyboardState newKeyboardState = Keyboard.GetState();

            // LMB was down and is now released, i.e. a left click.
            bool MouseClick = newMouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed;
            // Spacebar was done and is now up, i.e. it's been hit.
            bool SpacebarHit = newKeyboardState.IsKeyUp(Keys.Space) && oldKeyboardState.IsKeyDown(Keys.Space);

            if ((MouseClick || SpacebarHit))
            {
                Launch();
            }

            oldMouseState = newMouseState;
            oldKeyboardState = newKeyboardState;
        }

        public void Launch()
        {
            isActive = true;

            // TODO: both could return zero, i.e. stationary ball.
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

        private void ClampSpeed()
        {
            if (XVelocity > 10)
            {
                XVelocity = 10;
            }

            if (YVelocity > 10)
            {
                YVelocity = 10;
            }
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

            Double WallSpeedModifier = -1.0;

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
                isActive = false;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
