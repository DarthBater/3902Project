using Baritone.Input;
using Baritone.Input.Impl;
using Baritone.Wrappers.Listeners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers.Layers
{
    public class CollisionLayer : SpriteLayer
    {
        private Sprint4 game;

        public InputController MarioInputK
        {
            get; private set;
        }
        
        public InputController MarioInputG
        {
            get; private set;
        }

        private InputController PauseK;
        private InputController PauseG;

        public CollisionLayer(Sprint4 game) : base(1f)
        {
            this.game = game;
            this.name = "Collision";

            this.SortMode = SpriteSortMode.FrontToBack;
            this.BlendMode = BlendState.AlphaBlend;

            this.Initialize();
        }

        private void Initialize()
        {
            this.MarioInputK = new InputControllerKeyboard();
            this.MarioInputG = new InputControllerGamePad(PlayerIndex.One);

            this.PauseK = new InputControllerKeyboard();
            this.PauseG = new InputControllerGamePad(PlayerIndex.One);

            KeyListener pause = new PauseKeyListener(this.game, this.MarioInputK, this.MarioInputG);
            KeyListener right = new MarioMovementKeyListener(this.game, 1);
            KeyListener left = new MarioMovementKeyListener(this.game, -1);
            KeyListener jump = new MarioJumpKeyListener(this.game);
            KeyListener crouch = new MarioCrouchKeyListener(this.game);
            KeyListener dash = new MarioDashFireballKeyListener(this.game);
            KeyListener mute = new MuteListener(this.game);
            KeyListener volume = new VolumeListener(this.game);

            this.MarioInputK.RegisterKeyListener(Keys.Right, right);
            this.MarioInputK.RegisterKeyListener(Keys.Left, left);
            this.MarioInputK.RegisterKeyListener(Keys.D, right);
            this.MarioInputK.RegisterKeyListener(Keys.A, left);
            this.MarioInputK.RegisterKeyListener(Keys.W, jump);
            this.MarioInputK.RegisterKeyListener(Keys.Up, jump);
            this.MarioInputK.RegisterKeyListener(Keys.Down, crouch);
            this.MarioInputK.RegisterKeyListener(Keys.S, crouch);
            this.MarioInputK.RegisterKeyListener(Keys.Space, dash);
            
            this.MarioInputK.RegisterKeyListener(Keys.Y, new StandardMarioKeyListener(this.game));
            this.MarioInputK.RegisterKeyListener(Keys.U, new SuperMarioKeyListener(this.game));
            this.MarioInputK.RegisterKeyListener(Keys.I, new FireMarioKeyListener(this.game));

            

            this.MarioInputK.RegisterKeyListener(Keys.M, mute);
            this.MarioInputK.RegisterKeyListener(Keys.OemComma, volume);
            this.MarioInputK.RegisterKeyListener(Keys.OemPeriod, volume);

            this.MarioInputG.RegisterKeyListener(Buttons.DPadLeft, left);
            this.MarioInputG.RegisterKeyListener(Buttons.DPadRight, right);
            this.MarioInputG.RegisterKeyListener(Buttons.DPadUp, jump);
            this.MarioInputG.RegisterKeyListener(Buttons.DPadDown, crouch);
            this.MarioInputG.RegisterKeyListener(Buttons.B, dash);

            this.PauseK.RegisterKeyListener(Keys.P, pause);
            this.PauseG.RegisterKeyListener(Buttons.Start, pause);

            this.InputControllers.Add(this.MarioInputK);
            this.InputControllers.Add(this.MarioInputG);
            this.InputControllers.Add(this.PauseK);
            this.InputControllers.Add(this.PauseG);
        }
    }
}
