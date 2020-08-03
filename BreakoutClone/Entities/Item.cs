using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BreakoutClone.Entities
{
    class Item: IDrawable
    {
        Texture2D Image = Assets.Brick;

        Vector2 Position;

        public Rectangle Hitbox { get; set; }

        public bool IsVisible { get; set; }

        private readonly Random random = new Random();

        public Item()
        {
            // Randomly pick a spot for it to appear. 
            // TODO: the y-coordinate is particularly dumb.
            Position.X = random.Next(5, (int)(Breakout.ScreenSize.X - Image.Width - 5));
            Position.Y = random.Next(200, 500);

            IsVisible = true;

            Hitbox = new Rectangle(Position.ToPoint(), new Point(Image.Width, Image.Height));
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (IsVisible)
            {
                spritebatch.Draw(Image, Position, Color.White); 
            }
        }
    }
}
