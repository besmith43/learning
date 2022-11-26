using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Step_7___Sprite_Death_and_Respawn
{

	public class Sprite
	{
		protected Texture2D _texture;

		public Vector2 Position;
		public Vector2 Velocity;
		public float Speed;
		public Input Input;
		public bool IsRemoved = false;

		public Rectangle Rectangle
		{
			get
			{
				return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
			}
		}

		public Sprite(Texture2D texture)
		{
			_texture = texture;
		}

		public virtual void Update(GameTime gameTime, List<Sprite>sprites)
		{

		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, Position, Color.White);
		}
	}
}
