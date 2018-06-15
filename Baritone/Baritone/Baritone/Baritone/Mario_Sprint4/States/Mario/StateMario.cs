using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Baritone.States.Mario
{
    public abstract class StateMario
    {

        public StateMario PreviousState { get; private set; }
        public StateMachineMarioAction StateMachine { get; private set; }

        protected StateMario(StateMachineMarioAction stateMachine)
        {
            this.StateMachine = stateMachine;
            this.PreviousState = this;
        }

        public virtual void Enter(StateMario previousState)
        {
            if (previousState != null)
            {
                Console.WriteLine("Mario ActionState {0} -> {1}", previousState.GetType().Name, this.GetType().Name);
                previousState.Exit();
            }
            
            this.StateMachine.CurrentState = this;
            this.StateMachine.CurrentState.PreviousState = previousState;
        }

        public virtual void Exit()
        {
        }

        public abstract void ToIdle();
        public abstract void ToWalk(int direction);
        public abstract void ToRun();
        public abstract void StopH();
        public abstract void ToJump();
        public abstract void ToFall();
        public abstract void Crouch();
        public abstract void Uncrouch();
        public abstract void ToDead();

    }
}
