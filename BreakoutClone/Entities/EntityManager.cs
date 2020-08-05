using BreakoutClone.Content;
using BreakoutClone.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BreakoutClone
{

    class EntityManager : IDrawable
    {
        private List<IDrawable> Drawables;
        private List<IUpdate> Updaters;

        private List<Ball> ActiveBalls = new List<Ball>();

        private readonly List<Item> ActiveItems = new List<Item>();

        public Paddle Player { get; set; }

        Ball CurrentBall { get; set; }

        Wall CurrentWall { get; set; }

        private readonly Random random = new Random();

        public void CreateEntities()
        {
            CreateWall();

            CreatePlayer();

            CreateBall();

            AddEntitiesToLists();
        }

        private void AddEntitiesToLists()
        {
            Drawables = new List<IDrawable> { CurrentWall, Player, CurrentBall };
            Updaters = new List<IUpdate> { Player, CurrentWall };
        }

        private void CreatePlayer()
        {
            Player = new Paddle((int)(Breakout.ScreenSize.X / 2), 600, 100, 20);
        }

        private void CreateWall()
        {
            CurrentWall = new Wall();
            CurrentWall.Create(0, 100);
        }

        private void CreateBall()
        {
            CurrentBall = new Ball(new Vector2(200, 300), 3, 3);
            ActiveBalls.Add(CurrentBall);
        }

        // The HandleInput methods receive raw input from ScreenManager
        // and translate them into actions for entities to take.

        public void HandleInput(Keys key)
        {
            if (key == Keys.Space && (CurrentBall.IsActive == false))
            {
                CurrentBall.Launch();
            }
        }

        public void HandleInput(MouseState mouseState)
        {
            Player.MoveTo(mouseState.X);
        }

        public void HandleInput(bool WasThereAClick)
        {
            if (WasThereAClick)
            {
                CurrentBall.Launch();
            }
        }

        public void Update()
        {
            SpawnItems();

            UpdateEntities();

            RemakeWall();
        }

        private void UpdateEntities()
        {
            foreach (IUpdate updatable in Updaters)
            {
                updatable.Update();
            }

            foreach (Ball ball in ActiveBalls)
            {
                ball.Update(CurrentWall, ActiveItems, Player);
            }
        }

        private void RemakeWall()
        {
            if (CurrentWall.BricksLeft < 1)
            {
                CurrentWall.Create(0, 100);

                CurrentBall.Reset();
            }
        }

        private void SpawnItems()
        {
            // 1-in-600 chance for an item to spawn.
            // Must have destroyed some blocks before items spawn.
            // 3 max items.
            if (random.Next(1, 100) == 1 && CurrentWall.BricksLeft < 27 && ActiveItems.Count < 3)
            {
                Item item = ChoosePowerUp();

                Drawables.Add(item);
                ActiveItems.Add(item);
            }
        }

        private Item ChoosePowerUp()
        {
            switch (random.Next(4))
            {
                case 1:
                    return new PowerupBallSplit(CurrentBall, ActiveBalls);
                case 2:
                    return new PowerupDestroyBricks(CurrentWall);
                case 3:
                    return new PowerupPaddleLength(Player);
                default:
                    return new PowerupPaddleLength(Player);
            }
        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (IDrawable drawable in Drawables)
            {
                drawable.Draw(spritebatch);
            }
        }
    }
}
