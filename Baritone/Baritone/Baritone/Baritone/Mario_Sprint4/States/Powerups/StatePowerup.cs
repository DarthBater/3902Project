using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.States.Powerups
{
    public abstract class StatePowerup
    {

        public StatePowerup PreviousState { get; internal set; }
        public StateMachineMarioPowerup StateMachine { get; private set; }

        protected StatePowerup(StateMachineMarioPowerup stateMachine)
        {
            this.StateMachine = stateMachine;
            this.PreviousState = this;
        }

        public virtual void Enter(StatePowerup previousState)
        {
            if (previousState != null)
            {
                previousState.Exit();
            }

            this.StateMachine.CurrentState = this;
            this.StateMachine.CurrentState.PreviousState = previousState;
        }

        public virtual void Exit()
        {
        }

        public abstract void TakeDamage();
        public abstract void ReceiveMushroom();
        public abstract void ReceiveFireFlower();
        public abstract void ReceiveStar();

        public virtual void Update(GameTime gametime)
        {
        }
    }
}
