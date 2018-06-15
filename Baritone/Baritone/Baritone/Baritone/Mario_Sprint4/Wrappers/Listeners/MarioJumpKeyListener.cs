using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Baritone.Utils;
using Baritone.Sprites;

namespace Baritone.Wrappers.Listeners
{
    class MarioJumpKeyListener : KeyListener
    {

        public MarioJumpKeyListener(Sprint4 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            this.game.Mario.StateMachineAction.CurrentState.ToJump();
        }

        public override void OnRelease(int key)
        {
            float vy = this.game.Mario.Info.velocity.Y;
            if (vy < 0) //Going up
            {
                this.game.Mario.Info.velocity.Y /= 2;
            }
        }
    }
}
