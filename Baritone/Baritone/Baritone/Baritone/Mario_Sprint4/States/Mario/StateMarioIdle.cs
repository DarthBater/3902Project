using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Baritone.Utils;
using Baritone.Wrappers;

namespace Baritone.States.Mario
{
    public class StateMarioIdle : StateMario
    {

        public StateMarioIdle(StateMachineMarioAction stateMachine) : base(stateMachine)
        {
        }

        public override void Enter(StateMario previousState)
        {
            base.Enter(previousState);
            this.StateMachine.Mario.SetSpriteState(SpriteStates.Sprites.IDLE);
            this.StateMachine.Mario.Info.acceleration = Vector2.Zero;
            this.StateMachine.Mario.Info.velocity = Vector2.Zero;
        }

        public override void Exit()
        {

        }

        public override void ToIdle()
        {
        }

        public override void ToWalk(int direction)
        {
            this.StateMachine.Walking.Enter(this);
        }

        public override void ToRun()
        {

        }

        public override void StopH()
        {

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
            this.StateMachine.Mario.SetSpriteState(SpriteStates.Sprites.CROUCHING);
        }

        public override void Uncrouch()
        {
            this.StateMachine.Mario.SetSpriteState(SpriteStates.Sprites.IDLE);
        }

        public override void ToDead()
        {
            this.StateMachine.Dead.Enter(this);
        }

    }
}
