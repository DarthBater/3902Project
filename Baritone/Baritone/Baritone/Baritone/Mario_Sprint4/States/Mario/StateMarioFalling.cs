using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Baritone.Utils;
using Baritone.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Baritone.Wrappers;

namespace Baritone.States.Mario
{
    public class StateMarioFalling : StateMario
    {

        public StateMarioFalling(StateMachineMarioAction stateMachine) : base(stateMachine)
        {

        }

        public override void Enter(StateMario previousState)
        {
            base.Enter(previousState);

            this.StateMachine.Mario.SetSpriteState(SpriteStates.Sprites.JUMPING);
            this.StateMachine.Mario.Info.velocity.Y = .05f;
        }

        public override void Exit()
        {

        }

        public override void ToIdle()
        {
            this.StateMachine.Idle.Enter(this);
        }

        public override void ToWalk(int direction)
        {
            if (this.StateMachine.Mario.Info.velocity.X * direction <= 0)
            {
                this.StateMachine.Mario.Info.velocity.X += (direction * 1.5f);
            }
            else if (this.StateMachine.Mario.Info.velocity.X == 0)
            {
                this.StateMachine.Mario.Info.velocity.X += direction;
            }
            else
            {
                this.StateMachine.Mario.Info.velocity.X += .1f * direction;
            }

            (this.StateMachine.Walking as StateMarioWalking).previousX = this.StateMachine.Mario.Info.velocity.X;
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

        }

        public override void Crouch()
        {

        }

        public override void Uncrouch()
        {

        }

        public override void ToDead()
        {
            this.StateMachine.Dead.Enter(this);
        }

    }
}
