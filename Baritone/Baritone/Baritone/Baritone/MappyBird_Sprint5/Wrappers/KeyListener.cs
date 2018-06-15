using Baritone.MappyBird_Sprint5.Wrappers.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers
{
    public abstract class KeyListener
    {

        protected Layer Layer { get; private set; }

        protected KeyListener(Layer layer)
        {
            this.Layer = layer;
        }

        public abstract void OnPress(int key);
        public abstract void OnRelease(int key);
        public abstract void OnHold(int key);
    }
}
