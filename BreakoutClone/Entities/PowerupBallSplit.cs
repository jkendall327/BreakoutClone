using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone.Entities
{
    class PowerupBallSplit : Item
    {
        private Ball ball;

        public PowerupBallSplit(Ball ball)
        {
            this.ball = ball;
            Image = Assets.Powerup;
        }

        public override void Activate()
        {
            Vector2 pointOfOrigin = ball.Position;

            Ball newBall = new Ball(pointOfOrigin, (float)(ball.XVelocity * -1), (float)(ball.YVelocity * -1));
        }
    }
}
