using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Input.Impl
{
    public class InputControllerGamePad : InputController
    {

        private GamePadState previousState;
        private PlayerIndex index;

        public InputControllerGamePad(PlayerIndex index)
        {
            this.index = index;
            this.previousState = GamePad.GetState(index);
        }

        public override void ScanInputs()
        {
            if (!this.Paused)
            {
                GamePadState currentState = GamePad.GetState(this.index);

                foreach (int b in this.callbacks.Keys)
                {
                    if (currentState.IsButtonDown((Buttons) b))
                    {
                        if (!previousState.IsButtonDown((Buttons)b)) {
                            //Wasn't down, is now
                            this.callbacks[b].OnPress(b);
                        }
                        else
                        {
                            //Was down, is now
                            this.callbacks[b].OnHold(b);
                        }
                    }
                    else if (previousState.IsButtonDown((Buttons)b))
                    {
                        //was down, not now
                        this.callbacks[b].OnRelease(b);
                    }
                }
                
                this.previousState = currentState;
            }
        }
    }
}
