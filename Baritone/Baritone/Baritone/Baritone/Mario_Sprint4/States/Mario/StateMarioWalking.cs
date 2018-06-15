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
    public class StateMarioWalking : StateMario
    {

        public float previousX
        {
            internal set; get;
        }

        public int Direction
        {
            get; private set;
        }

        public StateMarioWalking(StateMachineMarioAction stateMachine) : base(stateMachine)
        {

        }

        public override void Enter(StateMario previousState)
        {
            base.Enter(previousState);

            this.StateMachine.Mario.SetSpriteState(SpriteStates.Sprites.WALKING);
            this.StateMachine.Mario.Info.velocity.X = this.previousX;
            this.StateMachine.Mario.Info.acceleration.X = 0.2f;
        }

        public override void Exit()
        {
            this.StateMachine.Mario.Info.acceleration.X = 0;
        }

        public override void ToIdle()
        {
            this.StateMachine.Idle.Enter(this);
            this.previousX = 0f; //Going from walk -> idle, start our speed over
        }

        public override void ToWalk(int direction)
        {
            this.Direction = direction;
            if (this.StateMachine.Mario.Info.acceleration.X == 0)
            {
                this.StateMachine.Mario.Info.acceleration.X = 0.2f;
            }

            this.StateMachine.Mario.Info.acceleration.X = Math.Abs(this.StateMachine.Mario.Info.acceleration.X) * direction;
            this.StateMachine.Mario.Info.velocity.X = Math.Abs(this.StateMachine.Mario.Info.velocity.X) * direction;
            this.StateMachine.Mario.Info.spriteEffects = direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        }

        public override void ToRun()
        {
            this.StateMachine.Mario.Info.velocity.X = MathHelper.Clamp(this.StateMachine.Mario.Info.velocity.X * 2, SpriteCollection.MIN_VELOCITY, SpriteCollection.MAX_VELOCITY);
        }

        public override void StopH()
        {
            this.ToIdle();
        }

        public override void ToJump()
        {
            this.StateMachine.Jump.Enter(this);
        }

        public override void ToFall()
        {
            this.StateMachine.Falling.Enter(this);
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
