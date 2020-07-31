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

        enum Direction { Left, Right };

        Direction playerDirection;

        public Input()
        {
            friction = 2;

            force = 0;

            speed = 0;

            playerDirection = new Direction();
        }

        public Vector2 UpdatePosition(Vector2 currentPosition)
        {
            if (speed > 0)
            {
                speed -= friction;
            }

            CheckInput();

            return CalculateFinalPosition(currentPosition);
        }

        private void CheckInput()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                playerDirection = Direction.Right;
                GenerateForce();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                playerDirection = Direction.Left;
                GenerateForce();
            }
        }

        private void GenerateForce()
        {
            force = 2;
            speed += force;

            if (speed >= 20)
            {
                speed = 20;
            }

        }

        private Vector2 CalculateFinalPosition(Vector2 originalPosition)
        {
            Vector2 finalPosition;
            switch (playerDirection)
            {
                case Direction.Left:
                    finalPosition = new Vector2(originalPosition.X - speed, originalPosition.Y);
                    break;
                case Direction.Right:
                    finalPosition = new Vector2(originalPosition.X + speed, originalPosition.Y);
                    break;
                default:
                    finalPosition = new Vector2(0, 0);
                    break;
            }

            Rectangle gameBoundaries = new Rectangle((int)Breakout.ScreenSize.X, (int)Breakout.ScreenSize.Y, Breakout.Viewport.Width, Breakout.Viewport.Height);

            //if (gameBoundaries.Contains(finalPosition) == false)
            //{
            //    finalPosition.X = 0;
            //    finalPosition.Y = 0;
            //}

            return finalPosition;
        }

    }

}
