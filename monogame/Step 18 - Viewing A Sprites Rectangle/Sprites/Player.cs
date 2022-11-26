using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Step_18___Viewing_A_Sprites_Rectangle
{

	public class Player : Sprite
	{
        public Player(GraphicsDevice graphicsDevice, Texture2D texture)
      : base(graphicsDevice, texture)
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Position.Y -= 3;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Position.Y += 3;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Position.X -= 3;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Position.X += 3;
        }
    }
}
