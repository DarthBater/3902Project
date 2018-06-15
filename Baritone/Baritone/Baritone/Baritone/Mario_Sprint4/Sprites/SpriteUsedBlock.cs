using Baritone.Utils;
using Baritone.Wrappers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Baritone.Sprites
{
    //I'm not sure if we actually need this class. Other block implementations
    //can modify their state to appear as a used block.

    //Certainly feel free to re-add this to the project if this is not the case.
    public class SpriteUsedBlock : SpriteBlock
    {
        public SpriteUsedBlock(Sprint1 game) : base(game)
        {
            this.Info.frameDelay = 10;

            this.RegisterState(States.USED_BLOCK, States.BLOCK_STILL, "Sprites/Blocks/used");
            this.SetSheetState(States.USED_BLOCK);
            this.SetSpriteState(States.BLOCK_STILL);
        }

        public override void Handle(int marioSheetState)
        {
            this.Bump();
        }

    }
}
