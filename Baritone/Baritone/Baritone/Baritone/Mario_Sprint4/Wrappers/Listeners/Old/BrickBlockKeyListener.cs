using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Baritone.Utils;
using Baritone.Sprites;

namespace Baritone.Wrappers.Listeners
{
    class BrickBlockKeyListener : KeyListener
    {

        public BrickBlockKeyListener(Sprint1 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            this.game.BlockBrick.Bump();
            this.game.BlockItem.Bump();
        }

        public override void OnRelease(int key)
        {
        }
    }
}
