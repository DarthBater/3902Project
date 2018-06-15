﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Baritone.Utils;
using Baritone.Sprites;

namespace Baritone.Wrappers.Listeners
{
    class StandardMarioKeyListener : KeyListener
    {

        public StandardMarioKeyListener(Sprint4 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            this.game.Mario.StateMachinePowerup.CurrentState.TakeDamage();
        }

        public override void OnRelease(int key)
        {
        }
    }
}
