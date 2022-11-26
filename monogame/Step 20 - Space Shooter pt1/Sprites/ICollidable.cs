using System;
using System.Collections.Generic;
using System.Text;

namespace Step_20___Space_Shooter_pt1.Sprites
{
    public interface ICollidable
    {
        void OnCollide(Sprite sprite);
    }
}
