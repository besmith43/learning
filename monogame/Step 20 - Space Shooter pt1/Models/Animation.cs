using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Step_20___Space_Shooter_pt1.Models
{
    public class Animation : ICloneable
    {
        public int CurrentFrame { get; set; }

        public int FrameCount { get; private set; }

        public int FrameHeight { get { return Texture.Height; } }

        public float FrameSpeed { get; set; }

        public int FrameWidth { get { return Texture.Width / FrameCount; } }

        public bool IsLooping { get; set; }

        public Texture2D Texture { get; private set; }

        public Animation(Texture2D texture, int frameCount)
        {
            Texture = texture;

            FrameCount = frameCount;

            IsLooping = true;

            FrameSpeed = 0.2f;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
