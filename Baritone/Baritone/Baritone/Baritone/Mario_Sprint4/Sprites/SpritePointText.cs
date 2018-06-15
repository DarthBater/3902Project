using Baritone.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;
using System.Threading;
using Baritone.States.Powerups;
using Microsoft.Xna.Framework.Audio;

namespace Baritone.Sprites
{

    class SpritePointText
    {
        public double currentTime;
        public SpriteText text
        {
            get; private set;
        }

        public SpritePointText(Sprint4 game, int points, SpriteCollection sprite)
        {
            this.text = new SpriteText(game, (int)sprite.Info.position.X, (int)sprite.Info.position.Y - 25, Color.Black);
            text.Text = delegate ()
            {
                return string.Format("+" + points);
            };
            currentTime = 0;
        }
    }
}
