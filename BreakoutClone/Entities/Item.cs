using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone.Entities
{
    class Item: IDrawable
    {
        Texture2D Image = Assets.Brick;

        Vector2 Position;

        public bool IsVisible { get; set; }

        private Random random = new Random();

        public Item()
        {
            // Randomly pick a spot for it to appear. 
            // TODO: the y-coordinate is particularly dumb.
            Position.X = random.Next(5, (int)(Breakout.ScreenSize.X - Image.Width - 5));
            Position.Y = random.Next(200, 500);

            IsVisible = true;
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
