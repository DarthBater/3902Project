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
    public class SpriteFloor : SpriteCollection, ICollidable
    {
        public SpriteFloor(Sprint4 game) : base(game)
        {
            SpriteUtils.SetDefaultProperties(this, "Sprites/Obstacles/floor");
        }

        public override bool isStatic()
        {
            return true;
        }

        public void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {
            if (!(other is SpriteFireball)) //handle fireball in fireball class
            {
                if (!(other is SpriteMario) || (other is SpriteMario && other.SpriteState != SpriteStates.Sprites.DEAD))
                {
                    if (!(other is IRevealable) || (direction != Direction.LEFT && direction != Direction.RIGHT))
                    {
                        CollisionHandler.HandleStop(direction, other, this);
                    }
                }
            }
            if (other is SpriteStar)
            {
                other.Info.velocity.Y = -5;
            }
        }
    }
}
