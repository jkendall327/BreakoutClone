using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone.Content
{
    class Assets
    {
        public static Texture2D Paddle;

        public void Load(ContentManager content)
        {
            Paddle = content.Load<Texture2D>("board");
        }
    }
}
