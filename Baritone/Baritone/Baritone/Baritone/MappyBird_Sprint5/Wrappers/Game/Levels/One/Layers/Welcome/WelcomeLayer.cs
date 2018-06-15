using Baritone.MappyBird_Sprint5.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Welcome
{
    public sealed class WelcomeLayer : Layer
    {

        public WelcomeLayer(Level level) : base(level, 0f, null, null)
        {
            Sprite welcome = SpriteFactory.CreateWelcomeScreen(this);
            Console.WriteLine(welcome.Width);
            welcome.Position = new Vector2(
                (this.Game.Viewport.Width / 2) - (welcome.Width / 2 * welcome.Scale),
                (this.Game.Viewport.Height / 2) - (welcome.Height / 2 * welcome.Scale) - 100);
            this.AddSprite(welcome);

        }


    }
}
