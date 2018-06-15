using Baritone.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.States.Powerups
{
    public class StateMachineMarioPowerup
    {

        public SpriteMario Mario { get; private set; }
        public StatePowerup CurrentState { get; internal set; }

        public StatePowerup Normal { get; private set; }
        public StatePowerup Super { get; private set; }
        public StatePowerup Fire { get; private set; }
        public StatePowerup Star { get; private set; }

        public StateMachineMarioPowerup(SpriteMario mario)
        {
            this.Mario = mario;

            this.Normal = new StatePowerupNormal(this);
            this.Super = new StatePowerupSuper(this);
            this.Fire = new StatePowerupFire(this);
            this.Star = new StatePowerupStar(this);

            this.CurrentState = this.Normal;
            this.CurrentState.Enter(null);
        }

    }
}
