using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BreakoutClone
{
    // https://www.dreamincode.net/forums/topic/143585-screen-manager-with-xna-part-1/

    public class MenuComponent : DrawableGameComponent
    {
        readonly string[] menuItems;
        int selectedIndex;

        Color normal = Color.White;
        Color highlight = Color.Yellow;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        MouseState mouseState;
        MouseState oldMouseState;
        readonly SpriteBatch spriteBatch;
        readonly SpriteFont spriteFont;

        Vector2 position;
        float width = 0f;
        float height = 0f;
        readonly float fontScale = 1;

        //List<Rectangle> menuItemHitboxes;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                if (selectedIndex < 0)
                    selectedIndex = 0;
                if (selectedIndex >= menuItems.Length)
                    selectedIndex = menuItems.Length - 1;
            }
        }

        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont spriteFont, string[] menuItems) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.spriteFont = spriteFont;
            this.menuItems = menuItems;
            MeasureMenu();
        }

        private void MeasureMenu()
        {
            height = 0;
            width = 0;

            foreach (string item in menuItems)
            {
                // The width of the menu is set to whatever the item with the longest width is.
                Vector2 size = spriteFont.MeasureString(item);
                if (size.X > width)
                    width = size.X;

                // Menu height is every item + 5 for each, as spacing.
                height += spriteFont.LineSpacing + 5;
            }

            position = new Vector2(
                (Game.Window.ClientBounds.Width - width) / 2,
                (Game.Window.ClientBounds.Height - height) / 2);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (Helper.CheckKey(Keys.Down, oldKeyboardState))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Length)
                    selectedIndex = 0;
            }
            if (Helper.CheckKey(Keys.Up, oldKeyboardState))
            {
                selectedIndex--;
                if (selectedIndex < 0)
                    selectedIndex = menuItems.Length - 1;
            }
            base.Update(gameTime);

            oldKeyboardState = keyboardState;

            mouseState = Mouse.GetState();



            oldMouseState = mouseState;

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Vector2 location = position;
            Color tint;

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedIndex)
                    tint = highlight;
                else
                    tint = normal;

                spriteBatch.DrawString(spriteFont, menuItems[i], location, tint, 0, Vector2.Zero, fontScale, SpriteEffects.None, 0);
                location.Y += spriteFont.LineSpacing + 5;
            }
        }
    }
}
