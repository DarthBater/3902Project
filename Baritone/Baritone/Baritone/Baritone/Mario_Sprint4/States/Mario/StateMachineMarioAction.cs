using Baritone.Sprites;
using Baritone.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.States.Mario
{
    public class StateMachineMarioAction
    {

        public SpriteMario Mario { get; private set; }
        public StateMario CurrentState { get; internal set; }

        public StateMario Idle { get; private set; }
        public StateMario Walking { get; private set; }
        public StateMario Jump { get; private set; }
        public StateMario Dead { get; private set; }
        public StateMario Falling { get; private set; }

        public StateMachineMarioAction(SpriteMario mario)
        {
            this.Mario = mario;

            this.Idle = new StateMarioIdle(this);
            this.Walking = new StateMarioWalking(this);
            this.Jump = new StateMarioJump(this);
            this.Dead = new StateMarioDead(this);
            this.Falling = new StateMarioFalling(this);

            this.CurrentState = this.Idle;
            this.CurrentState.Enter(null);
        }

    }
}
