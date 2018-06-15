using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Baritone.Utils;
using Baritone.Sprites;

namespace Baritone.Wrappers.Listeners
{
    class StarMarioKeyListener : KeyListener
    {

        public StarMarioKeyListener(Sprint1 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            if (this.game.Mario.SheetState != States.MARIO_INVINCIBLE_SUPER)
            {
                this.game.Mario.SetSheetState(States.MARIO_INVINCIBLE_SUPER);
            }
        }

        public override void OnRelease(int key)
        {
        }
    }
}
