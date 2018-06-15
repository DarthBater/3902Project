using Baritone.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace Baritone.States.Powerups
{
    public class StatePowerupStar : StatePowerup
    {

        private double remaining;

        public StatePowerupStar(StateMachineMarioPowerup stateMachine) : base(stateMachine)
        {

        }

        public override void Enter(StatePowerup previousState)
        {
            base.Enter(previousState);

            this.remaining = 10;

            if (previousState is StatePowerupNormal)
            {
                this.StateMachine.Mario.SetSheetState(SpriteStates.Sheets.STAR_NORMAL);
            }
            else
            {
                this.StateMachine.Mario.SetSheetState(SpriteStates.Sheets.STAR_SUPER);
            }

            MediaPlayer.Play(this.StateMachine.Mario.game.invincibleSong);
        }

        public override void ReceiveFireFlower()
        {
            SoundFactory.PlaySoundEffect(SoundFactory.PowerUp());

            if (this.PreviousState is StatePowerupNormal)
            {
                this.StateMachine.Mario.SetSheetState(SpriteStates.Sheets.STAR_SUPER);
                this.PreviousState = this.StateMachine.Super;
            }
            else if (this.PreviousState is StatePowerupSuper)
            {
                this.PreviousState = this.StateMachine.Fire;
            }

        }

        public override void ReceiveMushroom()
        {
            SoundFactory.PlaySoundEffect(SoundFactory.PowerUp());
            if (this.PreviousState is StatePowerupNormal)
            {
                this.StateMachine.Mario.SetSheetState(SpriteStates.Sheets.STAR_SUPER);
                this.PreviousState = this.StateMachine.Super;
            }
        }

        public override void ReceiveStar()
        {
            this.Enter(this.PreviousState);
        }

        public override void TakeDamage()
        {
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            this.remaining -= gametime.ElapsedGameTime.TotalSeconds;
            if (this.remaining <= 0)
            {
                Console.WriteLine("Star Remaining is <= 0. Returning to previous state");
                this.remaining = 0;
                this.PreviousState.Enter(this);
                MediaPlayer.Stop();
                MediaPlayer.Play(this.StateMachine.Mario.game.backgroundSong);
            }
        }

    }
}
