using Baritone.Utils;
using Baritone.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Baritone.Sprites
{
    public class SpritePipe : SpriteCollection, ICollidable
    {

        public SpritePipe(Sprint4 game) : base(game)
        {
            SpriteUtils.SetDefaultProperties(this, "Sprites/Obstacles/pipetop");
            this.Info.bounds = game.bounds;
            this.Info.spriteWidth = 32;
            this.Info.numFrames = 1;
        }

        public override bool isStatic()
        {
            return true;
        }

        public void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {
            CollisionHandler.HandleStop(direction, other, this);
        }
    }
}
