using BreakoutClone.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone.Entities
{
    class PowerupPaddleLength : Item
    {
        private Paddle paddle;
        public PowerupPaddleLength(Paddle paddle)
        {
            Image = Assets.Ball;
            this.paddle = paddle;
        }
        public override void Activate()
        {
            paddle.Width += 25;
        }
    }

}
