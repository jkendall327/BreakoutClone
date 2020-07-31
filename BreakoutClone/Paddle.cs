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
    class Paddle : IDrawable, ICollide, IUpdate
    {
        Texture2D Image;

        Rectangle Hitbox;

        Vector2 Position;

        Input input;

        public Paddle(Vector2 position)
        {
            Position = position;

            Image = Assets.Paddle;
            Hitbox = Image.Bounds;

            input = new Input();
        }

        public event EventHandler<EventArgs> Collision;

        public void CheckIfCollide(Rectangle bounds)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            Position = input.UpdatePosition(Position, Hitbox);

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
