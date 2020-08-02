using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BreakoutClone
{

    class EntityManager : IDrawable
    {
        private List<IDrawable> Drawables;
        private List<IUpdate> Updaters;

        Paddle Player { get; set; }

        Ball ActiveBall { get; set; }

        Wall ActiveWall { get; set; }

        public EntityManager()
        {

        }

        public void CreateEntities()
        {
            ActiveWall = new Wall();
            ActiveWall.Create(0, 100);

            Player = new Paddle(new Vector2(Breakout.ScreenSize.X / 2, 600));

            ActiveBall = new Ball(new Vector2(200, 300), 3, 3);

            ActiveBall.Subscribe(Player);

            // Add items to drawable, updatable lists.
            Drawables = new List<IDrawable> { ActiveWall, Player, ActiveBall };
            Updaters = new List<IUpdate> { Player, ActiveWall };
        }

        public void Update()
        {
            foreach (IUpdate updatable in Updaters)
            {
                updatable.Update();
            }

            if (ActiveWall.BricksLeft < 1)
            {
                ActiveWall.Create(0, 100);

                ActiveBall.Reset();
            }

            ActiveBall.Update(ActiveWall);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (IDrawable drawable in Drawables)
            {
                drawable.Draw(spritebatch);
            }
        }
    }
}
