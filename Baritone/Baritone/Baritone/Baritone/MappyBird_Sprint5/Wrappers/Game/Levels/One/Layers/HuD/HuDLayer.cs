using System;
using Baritone.MappyBird_Sprint5.Utils;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.GameOver
{
    public sealed class HuDLayer : Layer
    {

        public HuDLayer(Level level) : base(level, 0f, null, null)
        {
            SpriteText Score = new SpriteText(Game, 15, 15, Color.Black);
            SpriteText Time = new SpriteText(Game, 725, 15, Color.Black);

            Score.Text = delegate ()
            {
                return string.Format("Mappy\n {0}", level.Score);
            };

            Time.Text = delegate ()
            {
                return string.Format("Time\n {0}", level.timeLeft);
            };

            this.AddSpriteText(Score);
            this.AddSpriteText(Time);
        }


    }
}
