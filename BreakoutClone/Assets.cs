﻿using Microsoft.Xna.Framework.Content;
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

        public static Texture2D Brick;

        public static Texture2D Ball;

        public static void Load(ContentManager content)
        {
            Paddle = content.Load<Texture2D>("board");
            Brick = content.Load<Texture2D>("brick");
            Ball = content.Load<Texture2D>("ball");
        }
    }
}