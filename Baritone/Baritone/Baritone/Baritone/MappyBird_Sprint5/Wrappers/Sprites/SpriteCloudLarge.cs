using Baritone.MappyBird_Sprint5.Wrappers.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Sprites
{
    public  class SpriteCloudLarge : Sprite
    {

        public SpriteCloudLarge(Layer layer) : base(layer, 1, 1)
        {
            this.SetDefault("Sprites/Scenery/cloud_large");
        }

    }
}
