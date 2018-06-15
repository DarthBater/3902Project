using Baritone.Utils;
using Baritone.Wrappers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;

namespace Baritone.Sprites
{
    public class SpriteHiddenBlock : SpriteBlock, ICollidable
    {
        public SpriteHiddenBlock(Sprint4 game) : base(game)
        {
            this.Info.frameDelay = 10;

            this.RegisterState(SpriteStates.Sheets.HIDDEN, SpriteStates.Sprites.IDLE, "Sprites/Blocks/hidden");
            this.RegisterState(SpriteStates.Sheets.QUESTION, SpriteStates.Sprites.IDLE, "Sprites/Blocks/question");
            this.RegisterState(SpriteStates.Sheets.USED, SpriteStates.Sprites.IDLE, "Sprites/Blocks/used");
            this.SetSheetState(SpriteStates.Sheets.HIDDEN);
            this.SetSpriteState(SpriteStates.Sprites.IDLE);
        }

        public override bool isStatic()
        {
            return true;
        }

        public override void Handle()
        {
            if (this.SheetState != SpriteStates.Sheets.USED)
            {
                if (this.item != null)
                {
                    this.RevealItem();
                }
                base.Bump();
                this.SetSheetState(SpriteStates.Sheets.USED);
                Console.WriteLine(this.SheetState);
            }
            else
            {
                base.Bump();
            }
        }

        public override void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {
            //If from the bottom, call base. Base will handle it regardless of block type
            if (direction == Direction.BOTTOM && other is SpriteMario)
            {
                base.OnCollision(direction, other, intersection);
            }
            //If this hidden block has been hit already, call base, base will handle it (bump)
            else if (this.SheetState == SpriteStates.Sheets.USED)
            {
                base.OnCollision(direction, other, intersection);
            }
        }

    }
}
