using Baritone.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Baritone.States.Powerups
{
    public class StatePowerupSuper : StatePowerup
    {

        public StatePowerupSuper(StateMachineMarioPowerup stateMachine) : base(stateMachine)
        {

        }

        public override void Enter(StatePowerup previousState)
        {
            base.Enter(previousState);

            this.StateMachine.Mario.SetSheetState(SpriteStates.Sheets.SUPER);
            SoundFactory.PlaySoundEffect(SoundFactory.PowerUp());
        }

        public override void ReceiveFireFlower()
        {
            this.StateMachine.Fire.Enter(this);
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
            this.StateMachine.Normal.Enter(this);
            SoundFactory.PlaySoundEffect(SoundFactory.PowerDown());
        }

    }
}
