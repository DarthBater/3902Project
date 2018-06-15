using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Baritone.Utils;

namespace Baritone.Wrappers.Listeners
{
    class ResetBlockStatesKeyListener : KeyListener
    {

        public ResetBlockStatesKeyListener(Sprint1 game) : base(game)
        {
        }

        public override void OnHold(int key)
        {
        }

        public override void OnPress(int key)
        {
            game.RemoveSprite(game.BlockBrick);
            game.RemoveSprite(game.BlockHidden);
            game.RemoveSprite(game.BlockItem);
            game.RemoveSprite(game.BlockQuestion);
            game.RemoveSprite(game.BlockUsed);
            game.RemoveSprite(game.Mushroom);
            game.RemoveSprite(game.Star);

            game.Mushroom = SpriteFactory.CreateMushroom(game);
            game.Star = SpriteFactory.CreateStar(game);

            game.BlockBrick = SpriteFactory.CreateBrick(game);
            game.BlockBrick.SetPosition(450, 350);
            game.AddSprite(game.BlockBrick);

            game.BlockItem = SpriteFactory.CreateBrick(game);
            game.BlockItem.SetPosition(420, 350);
            game.BlockItem.SetItem(game.Mushroom);
            game.AddSprite(game.BlockItem);

            game.BlockHidden = SpriteFactory.CreateHiddenBlock(game);
            game.BlockHidden.SetPosition(500, 350);
            game.AddSprite(game.BlockHidden);

            game.BlockUsed = SpriteFactory.CreateUsedBlock(game);
            game.BlockUsed.SetPosition(550, 350);
            game.AddSprite(game.BlockUsed);

            game.BlockQuestion = SpriteFactory.CreateQuestionBlock(game);
            game.BlockQuestion.SetPosition(600, 350);
            game.AddSprite(game.BlockQuestion);
            game.BlockQuestion.SetItem(game.Star);
        }

        public override void OnRelease(int key)
        {
        }
    }
}
