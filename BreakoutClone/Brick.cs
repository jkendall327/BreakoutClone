using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone
{
    class Brick : IDrawable, IUpdate
    {
        Texture2D Image = Assets.Brick;

        Vector2 Position;

        public Rectangle Hitbox { get; set; }

        public bool isAlive { get; set; }

        public Brick(Vector2 position)
        {
            isAlive = true;

            Position = position;

            Hitbox = new Rectangle(Position.ToPoint(), new Point(Image.Width, Image.Height));
        }

        public Brick(int x, int y)
        {
            Position = new Vector2(x, y);

            Hitbox = Image.Bounds;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (isAlive)
            {
                spritebatch.Draw(Image, Position, Color.White); 
            }
        }

        public void Update()
        {
        }
    }
}
