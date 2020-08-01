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
        List<IDrawable> drawables = new List<IDrawable>();

        List<IUpdate> updaters = new List<IUpdate>();

        Paddle player;

        Ball ball;

        Wall wall;

        public EntityManager()
        {

        }

        public void CreateEntities()
        {
            wall = new Wall(0, 100);

            player = new Paddle(new Vector2(Breakout.ScreenSize.X / 2, 600));

            ball = new Ball(new Vector2(200, 300), 3, 3);

            ball.Subscribe(player);

            drawables.Add(wall);
            drawables.Add(player);
            drawables.Add(ball);

            updaters.Add(player);
        }

        public void Update()
        {
            foreach (IUpdate updatable in updaters)
            {
                updatable.Update();
            }

            ball.Update(wall);
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
