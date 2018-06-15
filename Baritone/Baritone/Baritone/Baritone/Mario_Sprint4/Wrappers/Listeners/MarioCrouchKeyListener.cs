﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Baritone.Utils;
using Baritone.Sprites;

namespace Baritone.Wrappers.Listeners
{
    class MarioCrouchKeyListener : KeyListener
    {

        public MarioCrouchKeyListener(Sprint4 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            if (this.game.Mario.SheetState != SpriteStates.Sheets.NORMAL)
            {
                this.game.Mario.StateMachineAction.CurrentState.Crouch();
            }
        }

        public override void OnRelease(int key)
        {
            this.game.Mario.StateMachineAction.CurrentState.Uncrouch();
        }
    }
}
