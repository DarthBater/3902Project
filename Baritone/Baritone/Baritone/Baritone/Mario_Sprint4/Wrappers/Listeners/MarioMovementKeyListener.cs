using Baritone.Sprites;
using Baritone.Utils;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers
{
    public class MarioMovementKeyListener : KeyListener
    {

        private int direction;
        private static int keysDown = 0;

        public MarioMovementKeyListener(Sprint4 game, int direction) : base(game)
        {
            this.direction = direction;
        }

        public override void OnHold(int key)
        {
            this.game.Mario.StateMachineAction.CurrentState.ToWalk(this.direction);
        }

        public override void OnPress(int key)
        {
            keysDown++;
        }

        public override void OnRelease(int key)
        {
            keysDown--;
            if (keysDown == 0)
            {
                Console.WriteLine("Released movement key");
                this.game.Mario.StateMachineAction.CurrentState.StopH();
            }
        }
    }
}
