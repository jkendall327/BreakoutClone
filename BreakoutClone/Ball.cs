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
    class Ball : IDrawable, IUpdate
    {
        Texture2D Image = Assets.Ball;

        Vector2 Position;

        Vector2 Destination;

        public Ball(Vector2 position)
        {
            Position = position;
        }

        public void Update()
        {
            Move();
        }

        private void Move()
        {

            Position.X += 1;
            Position.Y += 1;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
