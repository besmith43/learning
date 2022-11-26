using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Step_6___Sprite_Shooting_Bullets
{
    public class Ship : Sprite
    {
        public Bullet Bullet;

        public Ship(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            previouskey = currentkey;
            currentkey = Keyboard.GetState();

            if (currentkey.IsKeyDown(Keys.A))
            {
                _rotation -= MathHelper.ToRadians(RotationVelocity);
            }
            else if (currentkey.IsKeyDown(Keys.D))
            {
                _rotation += MathHelper.ToRadians(RotationVelocity);
            }

            Direction = new Vector2((float)Math.Cos(_rotation), (float)Math.Sin(_rotation));

            if (currentkey.IsKeyDown(Keys.W))
            {
                Position += Direction * LinearVelocity;
            }

            if (currentkey.IsKeyDown(Keys.Space) && previouskey.IsKeyUp(Keys.Space))
            {
                // Shoot
                AddBullet(sprites);
            }
        }

        private void AddBullet(List<Sprite> sprites)
        {
            var bullet = Bullet.Clone() as Bullet;
            bullet.Direction = this.Direction;
            bullet.Position = this.Position;
            bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 2f;
            bullet.parent = this;

            sprites.Add(bullet);
        }
    }
}
