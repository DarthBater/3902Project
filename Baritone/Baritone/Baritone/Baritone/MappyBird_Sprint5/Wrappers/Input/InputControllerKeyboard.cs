﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Input
{
    public class InputControllerKeyboard : InputController
    {

        private KeyboardState previousState;

        public InputControllerKeyboard() : base()
        {
            this.previousState = Keyboard.GetState(PlayerIndex.One);
        }

        public override void ScanInputs()
        {
            if (!this.Paused)
            {
                KeyboardState currentState = Keyboard.GetState();

                Keys[] pressedPrevious = previousState.GetPressedKeys();
                Keys[] pressedCurrent = currentState.GetPressedKeys();

                foreach (Keys k in pressedCurrent)
                {
                    if (this.Callbacks.ContainsKey((int)k))
                    {
                        if (!previousState.IsKeyDown(k))
                        {
                            //Wasn't down, is down now
                            this.Callbacks[(int)k].OnPress((int)k);
                        }
                        else
                        {
                            //Was down, is down now
                            this.Callbacks[(int)k].OnHold((int)k);
                        }
                    }
                }

                foreach (Keys k in pressedPrevious)
                {
                    if (this.Callbacks.ContainsKey((int)k))
                    {
                        if (!currentState.IsKeyDown(k))
                        {
                            //Was down, isn't now
                            this.Callbacks[(int)k].OnRelease((int)k);
                        }
                    }
                }
                this.previousState = currentState;
            }
        }
    }
}
