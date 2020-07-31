using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakoutClone
{
    class Input
    {
        /*
         * Do it like Doom did. Friction as a force constantly pulls against you.
         * Pressing on the movement keys gives you force in that direction.
         * As you hold the key down, force accumulates, increasing speed (to a max).
         * When you release the key, friction is free to degrade speed...
         * Until you're back to standstill.
         */

        int friction;

        int force;

        int speed;

        Vector2 nextPosition;

        Rectangle Hitbox;

        Rectangle ScreenSize = new Rectangle((int) Breakout.ScreenSize.X, (int) Breakout.ScreenSize.Y, Breakout.Viewport.Width, Breakout.Viewport.Height);

        public Input()
        {
            friction = 2;

            force = 0;

            speed = 0;

            playerDirection = new Direction();

        }

        public Vector2 UpdatePosition(Vector2 currentPosition, Rectangle hitbox)
        {

            if (speed < 0)
            {
                speed = 0;
            }
            if (speed > 0)
            {
                if (friction > speed)
                {
                    speed = 0;
                }

                speed -= friction;
            }

            CheckInput();

            return CalculateFinalPosition(currentPosition);
        }

        private void CheckInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                GenerateForce();
            }
        }

        private void GenerateForce()
        {
            force = 3;
            speed += force;

            if (speed >= 20)
            {
                speed = 20;
            }

        }

        private Vector2 CalculateFinalPosition(Vector2 originalPosition)
        {
            nextPosition.Y = originalPosition.Y;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                nextPosition.X += speed;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                nextPosition.X -= speed;
            }

            return nextPosition;
        }

    }

}
