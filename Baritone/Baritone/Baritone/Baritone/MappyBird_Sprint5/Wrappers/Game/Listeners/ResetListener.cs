using Baritone.MappyBird_Sprint5.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Listeners
{
    public class ResetListener : KeyListener
    {

        private Level level;

        public ResetListener(Level level) : base(null)
        {
            this.level = level;
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            Console.WriteLine("Reset. Calling Level.Restore");
            SoundHandler.StopLastSoundEffect();
            this.level.Restore();
        }

        public override void OnRelease(int key)
        {
        }
    }
}
