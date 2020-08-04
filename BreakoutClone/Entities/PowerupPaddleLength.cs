using BreakoutClone.Content;

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
            paddle.Width += 5;
        }
    }

}
