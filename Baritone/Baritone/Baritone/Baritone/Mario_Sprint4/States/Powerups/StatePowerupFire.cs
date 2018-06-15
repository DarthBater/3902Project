using Baritone.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Baritone.States.Powerups
{
    public class StatePowerupFire : StatePowerup
    {

        public StatePowerupFire(StateMachineMarioPowerup stateMachine) : base(stateMachine)
        {

        }

        public override void Enter(StatePowerup previousState)
        {
            base.Enter(previousState);

            this.StateMachine.Mario.SetSheetState(SpriteStates.Sheets.FIRE);
            SoundFactory.PlaySoundEffect(SoundFactory.PowerUp());
        }

        public override void ReceiveFireFlower()
        {
        }

        public override void ReceiveMushroom()
        {
        }

        public override void ReceiveStar()
        {
            this.StateMachine.Star.Enter(this);
        }

        public override void TakeDamage()
        {
            this.StateMachine.Super.Enter(this);
            SoundFactory.PlaySoundEffect(SoundFactory.PowerDown());
        }

    }
}
