using Baritone.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.States.Powerups
{
    public class StatePowerupNormal : StatePowerup
    {

        public StatePowerupNormal(StateMachineMarioPowerup stateMachine) : base(stateMachine)
        {
        }

        public override void Enter(StatePowerup previousState)
        {
            base.Enter(previousState);

            this.StateMachine.Mario.SetSheetState(SpriteStates.Sheets.NORMAL);
        }

        public override void ReceiveFireFlower()
        {
            if (!this.IsDead())
            {
                this.StateMachine.Super.Enter(this);
            }
        }

        public override void ReceiveMushroom()
        {
            if (!this.IsDead())
            {
                this.StateMachine.Super.Enter(this);
            }
        }

        public override void ReceiveStar()
        {
            if (!this.IsDead())
            {
                this.StateMachine.Star.Enter(this);
            }
        }

        public override void TakeDamage()
        {
            this.StateMachine.Mario.StateMachineAction.CurrentState.ToDead();
        }

        private bool IsDead()
        {
            return this.StateMachine.Mario.SpriteState == SpriteStates.Sprites.DEAD;
        }

    }
}
