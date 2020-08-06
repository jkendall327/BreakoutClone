using BreakoutClone.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BreakoutClone
{
    class StartScreen : GameScreen
    {
        MenuComponent menuComponent;
        Texture2D image;
        Rectangle imageRectangle;

        public int SelectedIndex
        {
            get { return menuComponent.SelectedIndex; }
            set { menuComponent.SelectedIndex = value; }
        }

        public StartScreen(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, Texture2D image) : base(game, spriteBatch)
        {
            string[] menuItems = { "Start Game", "Options", "End Game" };

            menuComponent = new MenuComponent(game, spriteBatch, spriteFont, menuItems);
            Components.Add(menuComponent);

            this.image = image;

            imageRectangle = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(image, imageRectangle, Color.White);
            base.Draw(gameTime);
        }

        public override void OnKeyPressed(object sender, KeyboardEventArgs keys)
        {
            if (keys.EventKeys.Contains(Keys.Escape))
            {
                game.Exit();
            }

            if (keys.EventKeys.Contains(Keys.Enter))
            {
                switch (SelectedIndex)
                {
                    case 0:
                        // Make a new version of actionScreen to set the gamestate clean.
                        //ResetGame();
                        break;
                    case 1:
                        OnScreenChanged(new GameScreenEventArgs(GameStates.Options));
                        break;
                    case 2:
                        game.Exit();
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
