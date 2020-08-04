using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BreakoutClone.Content
{
    public static class Assets
    {
        //public static Texture2D Paddle;

        public static Texture2D Brick;

        public static Texture2D Ball;

        public static Texture2D Powerup;

        public static void Load(ContentManager content)
        {
            //Paddle = content.Load<Texture2D>("board");
            Brick = content.Load<Texture2D>("brick");
            Ball = content.Load<Texture2D>("ball");
            Powerup = content.Load<Texture2D>("Powerup");
        }
    }
}
