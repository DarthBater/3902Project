using Baritone.MappyBird_Sprint5.Wrappers;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Background.Listeners
{
    public class MuteListener : KeyListener
    {

        public MuteListener(Layer layer) : base(layer)
        { }

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
