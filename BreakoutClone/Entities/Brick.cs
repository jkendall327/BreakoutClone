using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BreakoutClone
{
    class Brick : IDrawable, IUpdate
    {
        readonly Texture2D Image = Assets.Brick;

        Vector2 Position;

        public Rectangle Hitbox { get; set; }

        public bool IsAlive { get; set; }

        public Brick(Vector2 position)
        {
            IsAlive = true;

            Position = position;

            Hitbox = new Rectangle(Position.ToPoint(), new Point(Image.Width, Image.Height));
        }

        public void Draw(SpriteBatch spritebatch)
        {
            if (IsAlive)
            {
                spritebatch.Draw(Image, Position, Color.White);
            }
        }

        public void Update()
        {
        }
    }
}
