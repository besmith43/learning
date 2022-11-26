using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Step_13___Game_States
{

	public abstract class Component
	{
		public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

		public abstract void Update(GameTime gameTime);
	}
}
