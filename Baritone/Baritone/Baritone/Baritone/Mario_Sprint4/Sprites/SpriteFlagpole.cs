using Baritone.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Baritone.Sprites
{
    public class SpriteFlagpole : SpriteCollection
    {
        public SpriteFlagpole(Sprint4 game) : base(game)
        {
            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.IDLE, "MarioSprites/flagpole");
            this.SetSheetState(SpriteStates.Sheets.NORMAL);
            this.SetSpriteState(SpriteStates.Sprites.IDLE);
        }

        public override bool isStatic()
        {
            return true;
        }
    }
}
