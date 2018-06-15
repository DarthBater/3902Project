using Baritone.Wrappers;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Baritone.Input
{
    public abstract class InputController
    {
        public bool Paused { get; set; }
        protected Dictionary<int, KeyListener> callbacks
        {
            get; private set;
        }

        protected InputController()
        {
            this.callbacks = new Dictionary<int, KeyListener>();
        }

        public abstract void ScanInputs();

        private void RegisterKeyListener(int key, KeyListener listener)
        {
            if (!callbacks.ContainsKey(key))
            {
                this.callbacks.Add(key, listener);
            }
        }

        public void RegisterKeyListener(Buttons b, KeyListener listener)
        {
            this.RegisterKeyListener((int)b, listener);
        }

        public void RegisterKeyListener(Keys k, KeyListener listener)
        {
            this.RegisterKeyListener((int)k, listener);
        }

        public void RemoveKey(int key)
        {
            this.callbacks.Remove(key);
        }

    }
}
