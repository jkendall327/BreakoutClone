using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone
{
    class Paddle : IDrawable, IUpdate
    {
        Texture2D Image;

        public int Width { get; set; }

        public int Height { get; set; }

        Rectangle Hitbox;

        Vector2 Position;

        private KeyboardState oldKeyboardState;
        private MouseState oldMouseState;

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

            CheckKeyboard();         
        }

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

        private void CheckKeyboard()
        {
            KeyboardState newKeyboardState = Keyboard.GetState();

            if (newKeyboardState.IsKeyDown(Keys.Left))
            {
                MoveLeft();
            }
            if (newKeyboardState.IsKeyDown(Keys.Right))
            {
                MoveRight();
            }

            oldKeyboardState = newKeyboardState;
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
        }

        public void MoveLeft()
        {
            Position.X -= 5;

            if (Position.X < 1)
            {
                Position.X = 1;
            }
        }

        public void MoveRight()
        {
            Position.X += 5;

            if (Position.X + Width > Breakout.ScreenSize.X)
            {
                Position.X = Breakout.ScreenSize.X - Width;
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
