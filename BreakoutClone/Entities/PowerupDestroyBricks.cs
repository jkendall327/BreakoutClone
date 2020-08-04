using BreakoutClone.Content;
using System.Collections.Generic;

namespace BreakoutClone.Entities
{
    class PowerupDestroyBricks : Item
    {
        private Wall wall;

        public PowerupDestroyBricks(Wall wall)
        {
            this.wall = wall;
        }

        public override void Activate()
        {
            Brick[,] bricks = wall.BrickWall;

            // Select a random number of random bricks.

            int bricksToDestroy = random.Next(1, 3);

            for (int i = 0; i < bricksToDestroy; i++)
            {
                List<Brick> aliveBricks = new List<Brick>();
                foreach (Brick brick in bricks)
                {
                    if (brick.IsAlive)
                    {
                        aliveBricks.Add(brick);
                    }
                }

                if (aliveBricks.Count > 0)
                {
                    int brickToDestroy = random.Next(aliveBricks.Count);
                    aliveBricks[brickToDestroy].IsAlive = false;
                    wall.BricksLeft--;
                }
                // Crash is because the max value to the random function is less than zero.




            }
        }
    }
}
