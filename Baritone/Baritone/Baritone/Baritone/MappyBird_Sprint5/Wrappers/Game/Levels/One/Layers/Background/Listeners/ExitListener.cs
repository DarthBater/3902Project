using Baritone.MappyBird_Sprint5.Wrappers;
using Baritone.MappyBird_Sprint5.Wrappers.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.MappyBird_Sprint5.Wrappers.Game.Levels.One.Layers.Background.Listeners
{
    public class ExitListener : KeyListener
    {

        private Level level;

        public ExitListener(Level level) : base(null)
        {
            this.level = level;
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            this.level.Game.Exit();
        }

        public override void OnRelease(int key)
        {
        }
    }
}
