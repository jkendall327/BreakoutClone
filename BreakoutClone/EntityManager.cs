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

    class EntityManager : IDrawable
    {
        List<Brick> bricks = new List<Brick>();

        List<IDrawable> drawables = new List<IDrawable>();

        Paddle player;

        public EntityManager()
        {

        }

        public void CreateEntities()
        {
            player = new Paddle(new Vector2(Breakout.ScreenSize.X / 3, 400));

            bricks = CreateBricks();

            drawables.Add(player);
            foreach (Brick brick in bricks)
            {
                drawables.Add(brick);
            }
        }

        private List<Brick> CreateBricks()
        {
            return new List<Brick>() { new Brick(new Vector2(10, 50))};
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (IDrawable drawable in drawables)
            {
                drawable.Draw(spritebatch);
            }
        }
    }
}
