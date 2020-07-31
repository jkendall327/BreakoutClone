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
    class Ball : IDrawable, ICollide, IUpdate
    {
        Texture2D Image = Assets.Ball;

        Vector2 Position;

        Vector2 Destination;

        public Ball(Vector2 position)
        {
            Position = position;
        }

        public event EventHandler<EventArgs> Collision;

        public void CheckIfCollide(Rectangle bounds)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            Move();
        }

        private void Move()
        {

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
