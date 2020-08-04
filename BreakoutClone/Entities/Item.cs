using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BreakoutClone.Entities
{
    class Item : IDrawable
    {
        // A base abstract class that other powerups derive from?
        // So they can all be treated as Item at runtime.

        protected Texture2D Image = Assets.Brick;

        Vector2 Position;

        public Rectangle Hitbox { get; set; }

        public bool IsVisible { get; set; }

        protected readonly Random random = new Random();

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

        public virtual void Activate()
        {

        }
    }
}
