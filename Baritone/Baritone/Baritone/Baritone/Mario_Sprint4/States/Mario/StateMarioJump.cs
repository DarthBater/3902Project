using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Baritone.Utils;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Baritone.States.Mario
{
    public class StateMarioJump : StateMario
    {

        public StateMarioJump(StateMachineMarioAction stateMachine) : base(stateMachine)
        {
        }

        public override void Enter(StateMario previousState)
        {
            base.Enter(previousState);
            this.StateMachine.Mario.SetSpriteState(SpriteStates.Sprites.JUMPING);
            this.StateMachine.Mario.Info.velocity.Y = -8.5f;
            SoundFactory.PlaySoundEffect(SoundFactory.Jump());
        }

        public override void Exit()
        {
            this.StateMachine.Mario.Info.velocity.Y = 0;
        }

        public override void ToIdle()
        {
            bool crouching = this.StateMachine.Mario.SpriteState == SpriteStates.Sprites.CROUCHING;
            this.StateMachine.Idle.Enter(this);
            if (crouching)
            {
                this.StateMachine.Idle.Crouch();
            }
        }

        public override void ToWalk(int direction)
        {
            if (this.StateMachine.Mario.Info.velocity.X * direction <= 0) {
                this.StateMachine.Mario.Info.velocity.X += (direction * 1.5f);
            }
            else if(this.StateMachine.Mario.Info.velocity.X == 0)
            {
                this.StateMachine.Mario.Info.velocity.X += direction;
            }
            else
            {
                this.StateMachine.Mario.Info.velocity.X += .1f * direction;
            }

            this.StateMachine.Mario.Info.spriteEffects = direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

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
            this.StateMachine.Falling.Enter(this);
        }

        public override void Crouch()
        {
            this.StateMachine.Mario.SetSpriteState(SpriteStates.Sprites.CROUCHING);
        }

        public override void Uncrouch()
        {
            this.StateMachine.Mario.SetSpriteState(SpriteStates.Sprites.JUMPING);
        }

        public override void ToDead()
        {
            this.StateMachine.Dead.Enter(this);
        }

    }
}
