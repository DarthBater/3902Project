using Baritone.Utils;
using Baritone.Wrappers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework.Audio;

namespace Baritone.Sprites
{
    public class SpriteQuestionBlock : SpriteBlock
    {
        public SpriteQuestionBlock(Sprint4 game) : base(game)
        {
            this.Info.frameDelay = 12;
            this.RegisterState(SpriteStates.Sheets.QUESTION, SpriteStates.Sprites.IDLE, "Sprites/Blocks/question");
            this.RegisterState(SpriteStates.Sheets.USED, SpriteStates.Sprites.IDLE, "Sprites/Blocks/used");
            this.SetSheetState(SpriteStates.Sheets.QUESTION);
            this.SetSpriteState(SpriteStates.Sprites.IDLE);
        }

        public override bool isStatic()
        {
            return true;
        }

        public override void Handle()
        {
            if (this.SheetState == SpriteStates.Sheets.QUESTION)
            {
                this.RevealItem();
            }
            this.Bump();
        }

    }
}
