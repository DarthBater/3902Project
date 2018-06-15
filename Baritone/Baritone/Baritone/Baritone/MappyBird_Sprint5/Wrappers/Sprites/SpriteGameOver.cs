using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.MappyBird_Sprint5.Wrappers.Game;

namespace Baritone.MappyBird_Sprint5.Wrappers.Sprites
{
    public class SpriteGameOver: Sprite
    {

        public SpriteGameOver(Layer layer) : base(layer, 1, 1)
        {
            this.SetDefault("Sprites/HuD/youlose");
        }

    }
}
