﻿using BreakoutClone.Content;
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

        private List<Item> ActiveItems= new List<Item>();

        public Paddle Player { get; set; }

        Ball ActiveBall { get; set; }

        Wall ActiveWall { get; set; }

        private Random random = new Random();

        public EntityManager()
        {

        }

        public void CreateEntities()
        {
            ActiveWall = new Wall();
            ActiveWall.Create(0, 100);

            Player = new Paddle(new Vector2(Breakout.ScreenSize.X / 2, 600), 100, 20);

            ActiveBall = new Ball(new Vector2(200, 300), 3, 3);
            ActiveBall.Subscribe(Player);

            // Add items to drawable, updatable lists.
            Drawables = new List<IDrawable> { ActiveWall, Player, ActiveBall };
            Updaters = new List<IUpdate> { Player, ActiveWall };
        }

        // The HandleInput methods receive raw input from ScreenManager
        // and translate them into actions for entities to take.

        public void HandleInput(Keys key)
        {
            if (key == Keys.Right)
            {
                Player.MoveRight();
            }
            if (key == Keys.Left)
            {
                Player.MoveLeft();
            }

            if (key == Keys.Space)
            {
                if (ActiveBall.IsActive == false)
                {
                    ActiveBall.Launch();
                }
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
                ActiveBall.Launch();
            }
        }

        public void Update()
        {
            SpawnItems();

            foreach (IUpdate updatable in Updaters)
            {
                updatable.Update();
            }

            if (ActiveWall.BricksLeft < 1)
            {
                ActiveWall.Create(0, 100);

                ActiveBall.Reset();
            }

            ActiveBall.Update(ActiveWall, ActiveItems);

            if (Player.Width > 100)
            {
                Player.Width -= 10;
            }
        }

        private void SpawnItems()
        {
            // 1-in-600 chance for an item to spawn.
            // Must have destroyed some blocks before items spawn.
            // 3 max items.
            if (random.Next(1, 100) == 1 && ActiveWall.BricksLeft < 27 && ActiveItems.Count < 3)
            {
                Item item;
                if (random.Next() % 2 == 0)
                {
                    item = new PowerupPaddleLength(Player);
                }
                else
                {
                    item = new PowerupDestroyBricks(ActiveWall);
                }


                Drawables.Add(item);
                ActiveItems.Add(item);
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
