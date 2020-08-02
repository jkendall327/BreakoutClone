using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BreakoutClone
{

    class EntityManager : IDrawable
    {
        readonly List<IDrawable> drawables = new List<IDrawable>();
        readonly List<IUpdate> updaters = new List<IUpdate>();

        Paddle player;

        Ball ball;

        Wall wall;

        public EntityManager()
        {

        }

        public void CreateEntities()
        {
            wall = new Wall();
            wall.Create(0, 100);

            player = new Paddle(new Vector2(Breakout.ScreenSize.X / 2, 600));

            ball = new Ball(new Vector2(200, 300), 3, 3);

            ball.Subscribe(player);

            drawables.Add(wall);
            drawables.Add(player);
            drawables.Add(ball);

            updaters.Add(player);
            updaters.Add(wall);
        }

        public void Update()
        {
            foreach (IUpdate updatable in updaters)
            {
                updatable.Update();
            }

            if (wall.BricksLeft < 29)
            {
                wall.Create(0, 100);

                ball.Reset();
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
