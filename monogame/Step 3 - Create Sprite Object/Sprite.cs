using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Step_3___Create_Sprite_Object

{
    class Sprite
{
    private Texture2D _texture;
    public Vector2 Position;

    public float Speed = 2f;

    public Sprite(Texture2D texture)
    {
        _texture = texture;
    }

    public void Update()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.A))
        {
            Position.X -= Speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.D))
        {
            Position.X += Speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.S))
        {
            Position.Y += Speed;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.W))
        {
            Position.Y -= Speed;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_texture, Position, Color.White);
    }
}
}
