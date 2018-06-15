using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers
{
    public interface ICollidable
    {

        void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection);

    }
}
