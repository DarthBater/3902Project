using Baritone.Input;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Baritone.Utils;

namespace Baritone.Wrappers.Listeners
{
    class PauseKeyListener : KeyListener
    {

        private InputController[] toPause;

        public PauseKeyListener(Sprint4 game, params InputController[] toPause) : base(game)
        {
            this.toPause = toPause;
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {

            SoundFactory.PlaySoundEffect(SoundFactory.Pause());

            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Pause();
            }
            else
            {
                MediaPlayer.Resume();
            }

            foreach (SpriteLayer layer in this.game.Layers)
            {
                layer.Paused = !layer.Paused;
            }

            foreach (InputController ic in this.toPause)
            {
                ic.Paused = !ic.Paused;
            }
        }

        public override void OnRelease(int key)
        {

        }
    }
}
