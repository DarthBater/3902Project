using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers.Listeners
{
    class ExitKeyListener : KeyListener
    {

        public ExitKeyListener(Sprint4 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            this.game.Exit();
        }

        public override void OnRelease(int key)
        {

        }
    }
}
