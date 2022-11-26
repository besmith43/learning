using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Step_19___Per_Pixel_Collision_Detection_on_Rotated_Sprites
{
    public class Bullet : Sprite
    {
        private float _timer;

        public Bullet(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsRemoved = true;

            Position += Direction * LinearVelocity;
        }

        public override void OnCollide(Sprite sprite)
        {
            if (sprite == this.Parent)
                return;

            // Bullets don't collide with eachother
            if (sprite is Bullet)
                return;

            IsRemoved = true;
        }
    }
}
