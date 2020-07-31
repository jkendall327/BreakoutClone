﻿using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone
{
    class Paddle : IDrawable, ICollide
    {
        Texture2D Image;

        Rectangle Hitbox;

        Vector2 Position;

        public Paddle(Vector2 position)
        {
            Position = position;

            Image = Assets.Paddle;
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
    }
}