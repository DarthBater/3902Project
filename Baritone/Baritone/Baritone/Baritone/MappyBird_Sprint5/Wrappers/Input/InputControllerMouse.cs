using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Baritone.MappyBird_Sprint5.Wrappers.Input
{
    public class InputControllerMouse : InputController
    {

        private MouseState previousState;

        private Action onLeftClick = delegate() { };
        private Action onRightClick = delegate () { };
        private Action onMiddleClick = delegate () { };

        public InputControllerMouse()
        {
            this.previousState = Mouse.GetState();
        }

        public override void ScanInputs()
        {
            if (!this.Paused)
            {
                MouseState currentState = Mouse.GetState();

                if (currentState.LeftButton == ButtonState.Pressed)
                {
                    if (previousState.LeftButton != ButtonState.Pressed)
                    {
                        //Pressed, wasn't before
                        this.onLeftClick();
                    }
                }
                if (currentState.MiddleButton == ButtonState.Pressed)
                {
                    if (previousState.MiddleButton != ButtonState.Pressed)
                    {
                        //Pressed, wasn't before
                        this.onMiddleClick();
                    }
                }
                if (currentState.RightButton == ButtonState.Pressed)
                {
                    if (previousState.RightButton != ButtonState.Pressed)
                    {
                        //Pressed, wasn't before
                        this.onRightClick();
                    }
                }

                this.previousState = currentState;
            }
        }
        
        public void SetOnLeftClick(Action func)
        {
            this.onLeftClick = func;
        }

        public void SetOnRightClick(Action func)
        {
            this.onRightClick = func;
        }

        public void SetOnMiddleClick(Action func)
        {
            this.onMiddleClick = func;
        }

    }
}
