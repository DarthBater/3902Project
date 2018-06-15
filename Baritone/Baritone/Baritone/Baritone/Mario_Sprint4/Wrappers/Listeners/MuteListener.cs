using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers.Listeners
{
    public class MuteListener : KeyListener
    {

        public MuteListener(Sprint4 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            MediaPlayer.IsMuted = !MediaPlayer.IsMuted;
        }

        public override void OnRelease(int key)
        {
        }
    }
}
