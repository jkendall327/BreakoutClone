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

        private List<Ball> balls;

        public PowerupBallSplit(Ball ball, List<Ball> balls)
        {
            this.ball = ball;
            this.balls = balls;
            Image = Assets.Powerup;
        }

        public override void Activate()
        {
            if (balls.Count > 1)
            {
                Vector2 pointOfOrigin = ball.Position;

                Ball newBall = new Ball(pointOfOrigin, (float)(ball.XVelocity * -1), (float)(ball.YVelocity * -1));

                balls.Add(newBall); 
            }
        }
    }
}
