using Baritone.Sprites;
using Baritone.States.Mario;
using Baritone.Utils;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers
{
    public class MarioDashFireballKeyListener : KeyListener
    {

        public MarioDashFireballKeyListener(Sprint4 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
            if (this.game.Mario.SheetState != SpriteStates.Sheets.FIRE)
            {
                this.game.Mario.StateMachineAction.CurrentState.ToRun();
            }
        }

        public override void OnPress(int key)
        {
            if (this.game.Mario.SheetState == SpriteStates.Sheets.FIRE)
            {
                this.game.Mario.ThrowFireball();
            }
        }

        public override void OnRelease(int key)
        {
            if (this.game.Mario.SheetState != SpriteStates.Sheets.FIRE)
            {
                StateMario current = this.game.Mario.StateMachineAction.CurrentState;
                if (current is StateMarioWalking)
                {
                    (current as StateMarioWalking).ToWalk((current as StateMarioWalking).Direction);
                }
                //else, we aren't moving anyway
            }
        }
    }
}
