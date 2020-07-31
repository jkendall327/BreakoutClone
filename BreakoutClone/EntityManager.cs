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
            Paddle player = new Paddle();

            CreateBricks();
        }

        private void CreateBricks()
        {

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
