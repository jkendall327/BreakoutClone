using BreakoutClone.Content;
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

        int friction = 2;

        int force = 3;

        int speed;

        Vector2 nextPosition;

        public Vector2 UpdatePosition(Vector2 currentPosition)
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
                speed += force;

                if (speed >= 10)
                {
                    speed = 10;
                }
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
