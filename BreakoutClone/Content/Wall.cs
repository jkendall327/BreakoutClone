using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BreakoutClone.Content
{
    class Wall : IDrawable
    {
        public Brick[,] BrickWall { get; set; }

        public Wall(float x, float y)
        {
            BrickWall = new Brick[3, 10];

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

    }
}
