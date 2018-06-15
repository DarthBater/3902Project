using Baritone.MappyBird_Sprint5.Utils;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Background
{
    internal class BackgroundLayerMountains : Layer
    {

        public BackgroundLayerMountains(Level level) : base(level, 0.6f, SamplerState.LinearWrap, null)
        {
            this.Sprites.Add(SpriteFactory.CreateMountains(this));
        }

    }
}
