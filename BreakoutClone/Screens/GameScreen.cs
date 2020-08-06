using BreakoutClone.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace BreakoutClone
{
    public enum GameStates
    {
        Start, Options, Pause, Action
    }

    public class GameScreenEventArgs
    {
        public GameStates desiredScreen;

        public GameScreenEventArgs(GameStates screen)
        {
            this.desiredScreen = screen;
        }
    }

    public abstract class GameScreen : DrawableGameComponent
    {
        readonly List<GameComponent> components = new List<GameComponent>();
        protected Game game;
        protected SpriteBatch spriteBatch;

        public event EventHandler<GameScreenEventArgs> screenChanged;

        protected virtual void OnScreenChanged(GameScreenEventArgs screen)
        {
            screenChanged?.Invoke(this, screen);
        }

        public List<GameComponent> Components
        {
            get { return components; }
        }

        public GameScreen(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (GameComponent component in components)
                if (component.Enabled == true)
                    component.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (GameComponent component in components)
                if (component is DrawableGameComponent && ((DrawableGameComponent)component).Visible)
                    ((DrawableGameComponent)component).Draw(gameTime);
        }

        public virtual void Show()
        {
            this.Visible = true;
            this.Enabled = true;
            foreach (GameComponent component in components)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }

        public virtual void Hide()
        {
            this.Visible = false;
            this.Enabled = false;
            foreach (GameComponent component in components)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }

        public virtual void OnKeyPressed(object sender, KeyboardEventArgs keys)
        {
            // ...
        }

    }
}
