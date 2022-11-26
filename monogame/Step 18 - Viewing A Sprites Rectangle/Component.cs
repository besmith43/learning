using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Step_18___Viewing_A_Sprites_Rectangle
{

	public abstract class Component
	{
		public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

		public abstract void Update(GameTime gameTime);
	}
}
