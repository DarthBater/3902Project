using Baritone.Input;
using Baritone.Wrappers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Baritone.Input.Impl
{
    public class InputControllerImpl : InputController
    {

        private KeyboardState previousStateK;
        private GamePadState? previousStateG;
        private PlayerIndex? playerIndex;

        private Dictionary<Keys, KeyListener> callbacks = new Dictionary<Keys, KeyListener>();
        private Dictionary<Keys, Buttons> mirrors = new Dictionary<Keys, Buttons>();

        public InputControllerImpl(Sprint3 game, PlayerIndex? playerIndex) : base(game)
        {
            this.previousStateK = Keyboard.GetState();
            if (playerIndex != null)
            {
                this.previousStateG = GamePad.GetState(playerIndex.Value);
            }
            if (playerIndex.HasValue)
            {
                this.playerIndex = playerIndex.Value;
            }
        }

        public void RemoveKey(Keys key)
        {
            callbacks.Remove(key);
        }

        public void RegisterKeyListener(Keys key, KeyListener listener)
        {
            callbacks.Remove(key);
            callbacks.Add(key, listener);
        }

        public void MirrorKey(Keys key, Buttons button)
        {
            this.mirrors.Remove(key);
            this.mirrors.Add(key, button);
        }

        public void copy(InputControllerImpl other)
        {
            foreach (Keys k in other.callbacks.Keys)
            {
                this.callbacks.Add(k, other.callbacks[k]);
            }

            foreach (Keys k in other.mirrors.Keys)
            {
                this.mirrors.Add(k, other.mirrors[k]);
            }
        }

        public override void ScanInputs()
        {
            KeyboardState currentStateK = Keyboard.GetState();
            GamePadState? currentStateG = null;
            if (this.playerIndex != null)
            {
                currentStateG = GamePad.GetState(this.playerIndex.Value);
            }


            List<Keys> keys = new List<Keys>();
            List<Keys> keysPrevious = new List<Keys>();

            if (this.playerIndex == null)
            {
                foreach (Keys k in currentStateK.GetPressedKeys())
                    keys.Add(k);
            }
            else
            {
                foreach (Keys k in this.mirrors.Keys)
                {
                    if (currentStateG.Value.IsButtonDown(this.mirrors[k]))
                    {
                        keys.Add(k);
                    }
                }
            }

            if (this.playerIndex == null)
            {
                foreach (Keys k in previousStateK.GetPressedKeys())
                    keysPrevious.Add(k);
            }
            else
            {
                foreach (Keys k in this.mirrors.Keys)
                {
                    if (previousStateG.Value.IsButtonDown(this.mirrors[k]))
                    {
                        keysPrevious.Add(k);
                    }
                }
            }

            if (currentStateG.HasValue)
            {
                foreach (Keys k in this.mirrors.Keys)
                {
                    if (currentStateG.Value.IsButtonDown(this.mirrors[k]))
                    {
                        keys.Add(k);
                    }
                    if (previousStateG.Value.IsButtonDown(this.mirrors[k]))
                    {
                        keysPrevious.Add(k);
                    }
                }
            }

            foreach (Keys pressed in keys)
            {
                if (this.callbacks.ContainsKey(pressed))
                {
                    bool isDownNow = currentStateK.IsKeyDown(pressed);
                    bool isDownPrev = previousStateK.IsKeyDown(pressed);

                    if (this.playerIndex != null)
                    {
                        if (!isDownNow && mirrors.ContainsKey(pressed))
                        {
                            isDownNow = currentStateG.Value.IsButtonDown(mirrors[pressed]);
                        }

                        if (!isDownPrev && mirrors.ContainsKey(pressed))
                        {
                            isDownPrev = previousStateG.Value.IsButtonDown(mirrors[pressed]);
                        }
                    }

                    if (isDownNow)
                    {
                        if (!isDownPrev)
                        {
                            if (this.callbacks.ContainsKey(pressed))
                            {
                                this.callbacks[pressed].OnPress(pressed);
                            }
                        }
                        else
                        {
                            if (this.callbacks.ContainsKey(pressed))
                            {
                                this.callbacks[pressed].OnHold(pressed);
                            }
                        }
                    }
                }
            }

            foreach (Keys pressed in keysPrevious)
            {
                if (this.callbacks.ContainsKey(pressed))
                {
                    if (this.playerIndex == null)
                    {
                        if (!currentStateK.IsKeyDown(pressed))
                        {
                            //released
                            this.callbacks[pressed].OnRelease(pressed);
                        }
                    }
                    else if (this.mirrors.ContainsKey(pressed))
                    {
                        if (!currentStateG.Value.IsButtonDown(this.mirrors[pressed]))
                        {
                            this.callbacks[pressed].OnRelease(pressed);
                        }
                    }
                }
            }

            this.previousStateK = currentStateK;
            this.previousStateG = currentStateG;
        }
    }
}
