using Baritone.Utils;
using Baritone.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Baritone.Sprites
{
    public class SpriteYouWin : SpriteCollection
    {

        public SpriteYouWin(Sprint4 game) : base(game)
        {
            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.IDLE, "Sprites/HuD/youwin");
            this.SetSheetState(SpriteStates.Sheets.NORMAL);
            this.SetSpriteState(SpriteStates.Sprites.IDLE);
            this.Info.spriteWidth = this.current.Width;
            this.Info.numFrames = 1;
        }

        public override bool isStatic()
        {
            return true;
        }



    }
}
