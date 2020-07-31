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
    class Brick : IDrawable, ICollide, IUpdate
    {
        Texture2D Image = Assets.Brick;

        Vector2 Position;

        Rectangle Hitbox;

        public Brick(Vector2 position)
        {
            Position = position;

            Hitbox = Image.Bounds;
        }

        public Brick(int x, int y)
        {
            Position = new Vector2(x, y);

            Hitbox = Image.Bounds;
        }

        public event EventHandler<EventArgs> Collision;

        public void CheckIfCollide(Rectangle bounds)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }

        public void Update()
        {
        }
    }
}
