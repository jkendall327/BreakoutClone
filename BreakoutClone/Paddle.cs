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
    class Paddle : IDrawable
    {
        Texture2D Image = Assets.Paddle;

        Rectangle Hitbox;

        public Paddle()
        {

        }

        public void Draw(SpriteBatch spritebatch)
        {
            throw new NotImplementedException();
        }
    }
}
