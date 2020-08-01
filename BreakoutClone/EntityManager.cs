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

        List<IUpdate> updaters = new List<IUpdate>();

        Paddle player;

        Ball ball;

        public EntityManager()
        {

        }

        public void CreateEntities()
        {
            player = new Paddle(new Vector2(Breakout.ScreenSize.X / 2, 600));

            ball = new Ball(new Vector2(200, 300), 3, 3);

            ball.Subscribe(player);

            bricks = CreateBricks(8);

            drawables.Add(player);
            drawables.Add(ball);
            foreach (Brick brick in bricks)
            {
                drawables.Add(brick);
            }

            updaters.Add(player);
            updaters.Add(ball);
            foreach (Brick brick in bricks)
            {
                updaters.Add(brick);
            }
        }

        private List<Brick> CreateBricks(int numberOfRows)
        {
            var bricks = new List<Brick>();

            int yCoordinate = 50;
            for (int i = 0; i < numberOfRows; i++)
            {
                var row = CreateRow(yCoordinate);
                bricks.AddRange(row);
                yCoordinate += Assets.Brick.Height;
            }

            return bricks;
        }

        private List<Brick> CreateRow(int yCoordinate)
        {
            int numberOfBricks = (int)(Breakout.ScreenSize.Length() / Assets.Brick.Width);

            var row = new List<Brick>();

            int xCoordinate = 0;

            for (int i = 0; i < numberOfBricks; i++)
            {
                row.Add(new Brick(xCoordinate, yCoordinate));
                xCoordinate += Assets.Brick.Width;
            }

            return row;
        }

        public void Update()
        {
            foreach (IUpdate updatable in updaters)
            {
                updatable.Update();
            }
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
