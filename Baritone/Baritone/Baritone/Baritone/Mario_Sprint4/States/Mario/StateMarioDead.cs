using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Baritone.Utils;
using Baritone.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Baritone.Wrappers;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Threading.Tasks;
using System.Threading;

namespace Baritone.States.Mario
{
    public class StateMarioDead : StateMario
    {
       

        public StateMarioDead(StateMachineMarioAction stateMachine) : base(stateMachine)
        {

        }

        public override void Enter(StateMario previousState)
        {
            base.Enter(previousState);

            this.StateMachine.Mario.SetSheetState(SpriteStates.Sheets.NORMAL);
            this.StateMachine.Mario.SetSpriteState(SpriteStates.Sprites.DEAD);
            this.StateMachine.Mario.Info.velocity = new Vector2(0f, -8f);

            MediaPlayer.Stop();
            SoundEffect dying = SoundFactory.Death();
            SoundFactory.PlaySoundEffect(dying);
            Task.Factory.StartNew(delegate() {
                Console.WriteLine("Waiting {0}ms before resetting", dying.Duration.Milliseconds * 10);
                Thread.Sleep(dying.Duration.Milliseconds * 10);

                this.StateMachine.Mario.lives--;

                if (this.StateMachine.Mario.lives > 0)
                {
                    Console.WriteLine("Resetting after next update loop");
                    this.StateMachine.Mario.SetSpriteState(SpriteStates.Sprites.IDLE);
                    this.StateMachine.Mario.game.RunAfterUpdate = this.StateMachine.Mario.game.Reset;
                }
                //else, mediaplayer remains stopped
            });
        }

        public override void Exit()
        {

        }

        public override void ToIdle()
        {
             
        }

        public override void ToWalk(int direction)
        {

        }

        public override void ToRun()
        {

        }

        public override void StopH()
        {

        }

        public override void ToJump()
        {

        }

        public override void ToFall()
        {

        }

        public override void Crouch()
        {

        }

        public override void Uncrouch()
        {

        }

        public override void ToDead()
        {

        }

    }
}
