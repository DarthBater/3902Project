using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Baritone.Utils;
using Baritone.Sprites;

namespace Baritone.Wrappers.Listeners
{
    class LandOnKoopaListener : KeyListener
    {

        public LandOnKoopaListener(Sprint1 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            this.game.KoopaGreen.LandOnKoopa();
        }

        public override void OnRelease(int key)
        {
        }
    }
}
