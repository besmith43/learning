using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Step_5___Rotating_Sprite
{

	public class Sprite
	{
		private Texture2D _texture;
		public float _rotation;

		public Vector2 Position;
		public Vector2 Origin;

		public float RotationVelocity = 3f;
		public float LinearVelocity = 4f;

		public Sprite(Texture2D texture)
		{
			_texture = texture;
		}

		public void Update()
		{
			if (Keyboard.GetState().IsKeyDown(Keys.A))
			{
				_rotation -= MathHelper.ToRadians(RotationVelocity);
			}
			if (Keyboard.GetState().IsKeyDown(Keys.D))
			{
				_rotation += MathHelper.ToRadians(RotationVelocity);
			}

			var direction = new Vector2((float)Math.Cos(MathHelper.ToRadians(90) - _rotation), -(float)Math.Sin(MathHelper.ToRadians(90) - _rotation));

			if (Keyboard.GetState().IsKeyDown(Keys.W))
			{
				Position += direction * LinearVelocity;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.S))
			{
				Position -= direction * LinearVelocity;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(_texture, Position, null, Color.White, _rotation, Origin, 1, SpriteEffects.None, 0f);
		}
	}
}
