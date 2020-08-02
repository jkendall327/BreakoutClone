using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BreakoutClone
{
    //TODO: next task is drawing the paddle dynamically so the size can be changed mid-game.

    class Paddle : IDrawable, IUpdate
    {
        readonly Texture2D Image;

        public int Width { get; set; }

        public int Height { get; set; }

        Rectangle Hitbox;

        Vector2 Position;

        private KeyboardState oldKeyboardState;
        private MouseState oldMouseState;

        public event EventHandler<Rectangle> PaddleMoved;

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
            CheckMouse();

        }

        /*
         * TODO: Move this code out of here and into ScreenManager?
         * Consolidate all the raw input checking into one place.
         * Then pass those keys down to entitymanager through to the paddle.
         */

        private void CheckMouse()
        {
            MouseState newmouseState = Mouse.GetState();

            if (oldMouseState.X != newmouseState.X)
            {
                if (Breakout.Viewport.Bounds.Contains(newmouseState.Position))
                {
                    MoveTo(newmouseState.X);
                }
            }

            oldMouseState = newmouseState;
        }

        public void MoveTo(float xCoordinate)
        {
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

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
