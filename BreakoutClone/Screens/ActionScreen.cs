using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BreakoutClone.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BreakoutClone
{
	class ActionScreen : GameScreen
	{
		Texture2D image;
		Rectangle imageRectangle;

        public EntityManager EntitiesManager { get; private set; }

		public ActionScreen(Game game, SpriteBatch spriteBatch, Texture2D image) : base(game, spriteBatch)
		{
			this.image = image;
			imageRectangle = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);

			EntitiesManager = new EntityManager();
			EntitiesManager.CreateEntities();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			EntitiesManager.Update();
		}

		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Draw(image, imageRectangle, Color.White);
			base.Draw(gameTime);
			EntitiesManager.Draw(spriteBatch);

		}
	}
}
