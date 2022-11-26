using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Step_20___Space_Shooter_pt1.Models;

namespace Step_20___Space_Shooter_pt1.Sprites
{
    public class Explosion : Sprite
    {
        private float _timer = 0f;

        public Explosion(Dictionary<string, Animation> animations) : base(animations)
        {

        }

        public override void Update(GameTime gameTime)
        {
            _animationManager.Update(gameTime);

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_timer > _animationManager.CurrentAnimation.FrameCount * _animationManager.CurrentAnimation.FrameSpeed)
                IsRemoved = true;
        }
    }
}
