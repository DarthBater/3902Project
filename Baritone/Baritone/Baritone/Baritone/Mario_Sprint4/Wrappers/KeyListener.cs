using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers
{
    public abstract class KeyListener
    {

        protected Sprint4 game
        {
            get; private set;
        }

        protected KeyListener(Sprint4 game)
        {
            this.game = game;
        }

        public abstract void OnPress(int key);
        public abstract void OnRelease(int key);
        public abstract void OnHold(int key);
    }
}
