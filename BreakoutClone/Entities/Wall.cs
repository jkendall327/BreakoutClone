using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BreakoutClone.Content
{
    class Wall : IDrawable, IUpdate
    {
        public Brick[,] BrickWall { get; set; }

        public int BricksLeft { get; set; }

        public int Columns { get; set; }

        public int Rows { get; set; }

        public Wall()
        {
        }

        public void Create(float x, float y)
        {
            Rows = 3;
            Columns = 10;

            BrickWall = new Brick[Rows, Columns];

            BricksLeft = BrickWall.Length;

            // Loop for setting the columns.
            for (int i = 0; i < 3; i++)
            {
                // The y-coordinate is the origin point of the wall,
                // added to the number of columns * brick height.
                // Same logic for rows.
                int yCoordinate = (int)(y + i * Assets.Brick.Height);

                // Loop for setting the rows.
                for (int j = 0; j < 10; j++)
                {
                    int xCoordinate = (int)(x + j * Assets.Brick.Width);

                    Brick brick = new Brick(new Vector2(xCoordinate, yCoordinate));

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

        }

        public void Update()
        {

        }
    }
}
