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
        Rectangle NextHitbox;

        Vector2 Position;

        Vector2 NextPosition;

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
            NextPosition = input.UpdatePosition(Position);
            NextHitbox = new Rectangle(NextPosition.ToPoint(), new Point(Hitbox.Width, Hitbox.Height));

            if (new Rectangle(0, 0, 500, 700).Contains(NextHitbox))
            {
                Position = NextPosition;
                Hitbox = NextHitbox;
            }

            Console.WriteLine(Position.ToString());
            Console.WriteLine(Hitbox.ToString());
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Image, Position, Color.White);
        }
    }
}
