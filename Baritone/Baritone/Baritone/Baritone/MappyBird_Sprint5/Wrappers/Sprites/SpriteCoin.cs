using Baritone.Bird_Sprint5;
using Baritone.MappyBird_Sprint5.Utils;
using Baritone.MappyBird_Sprint5.Wrappers.Game;
using Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Sprites
{
    public class SpriteCoin : Sprite, ICollidable
    {

        public SpriteCoin(Layer layer) : base(layer, 2, 300)
        {
            this.SetDefault("Sprites/Items/coin");
        }
        public void OnCollision(Direction d, Sprite other, Rectangle intersection)
        {
            if(other is SpriteMappy)
            {
                this.Layer.RemoveSprite(this);
            }
            SoundHandler.PlaySoundEffect((this.Level as DynamicLevel).L1SoundHandler.Coin);
            this.Level.timeLeft += 2;
        }
    }
}
