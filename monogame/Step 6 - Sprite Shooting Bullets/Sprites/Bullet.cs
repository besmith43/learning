using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Step_6___Sprite_Shooting_Bullets
{
    public class Bullet : Sprite
    {
        private float _timer;

        public Bullet(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > LifeSpan)
            {
                IsRemoved = true;
            }

            Position += Direction * LinearVelocity;
        }
    }
}
