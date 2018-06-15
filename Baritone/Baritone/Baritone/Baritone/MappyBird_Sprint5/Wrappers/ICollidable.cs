using Baritone.Bird_Sprint5;
using Baritone.MappyBird_Sprint5.Wrappers.Game;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers
{
    public interface ICollidable
    {

        void OnCollision(Direction d, Sprite other, Rectangle intersection);

    }
}
