using Baritone.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Baritone.Sprites
{
    public class SpriteStair : SpriteCollection, ICollidable
    {
        public SpriteStair(Sprint4 game) : base(game)
        {
            SpriteUtils.SetDefaultProperties(this, "Sprites/Obstacles/stair");
        }

        public override bool isStatic()
        {
            return true;
        }

        public void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {

            if (other is SpriteMario && ((other as SpriteMario).won))
            {
                CollisionHandler.HandleStop(direction, other, this);
                return;
            }

            if (!(other is SpriteMario) || (other is SpriteMario && other.SpriteState != SpriteStates.Sprites.DEAD))
            {
                CollisionHandler.HandleStop(direction, other, this);
            }

            if (other is SpriteStar && direction == Direction.TOP)
            {
                other.Info.velocity.Y = -5;
            }
        }
    }
}
