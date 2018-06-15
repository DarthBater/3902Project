using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace Baritone.Wrappers.Listeners
{
    class ResetKeyListener : KeyListener
    {
        public ResetKeyListener(Sprint4 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            this.game.Reset();
            this.game.CameraX = 0;
            this.game.Mario.Info.bounds = game.bounds;
            this.game.Mario.SetPosition(0, game.bounds.Bottom - 150);
            this.game.Mario.lives = 3;
            this.game.Mario.points = 0;
            this.game.Mario.coins = 0;
            this.game.Mario.nextLife = 1;
            this.game.checkpoint = 0;
            MediaPlayer.Play(this.game.backgroundSong);
        }

        public override void OnRelease(int key)
        {

        }
    }
}
