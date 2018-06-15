using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Wrappers.Listeners
{
    public class VolumeListener : KeyListener
    {

        public VolumeListener(Sprint4 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            if (key == (int) Keys.OemComma)
            {
                MediaPlayer.Volume -= 0.1f;
            }
            else if (key == (int) Keys.OemPeriod)
            {
                MediaPlayer.Volume += 0.1f;
                                                           }
        }

        public override void OnRelease(int key)
        {
        }
    }
}
