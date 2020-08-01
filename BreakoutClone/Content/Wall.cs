using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone.Content
{
    class Wall : IDrawable
    {
        public Brick[,] BrickWall { get; set; }

        public Wall(float x, float y)
        {
            BrickWall = new Brick[3, 10];

            for (int i = 0; i < 3; i++)
            {
                y += i * Assets.Brick.Height;

                for (int j = 0; j < 10; j++)
                {
                    x += j * Assets.Brick.Width;

                    Brick brick = new Brick(new Vector2(x, y));

                    BrickWall[i, j] = brick;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Brick brick in BrickWall)
            {
                brick.Draw(spriteBatch);
            }

            //for (int i = 0; i < 7; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {
            //        BrickWall[i, j].Draw(spriteBatch);
            //    }
            //}
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
    }
}
