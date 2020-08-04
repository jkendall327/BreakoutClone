using BreakoutClone.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            int bricksToDestroy = random.Next(1, 10);

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

                int brickToDestroy = random.Next(aliveBricks.Count - 1);

                aliveBricks[brickToDestroy].IsAlive = false;
                wall.BricksLeft--;

            }
        }
    }
}
