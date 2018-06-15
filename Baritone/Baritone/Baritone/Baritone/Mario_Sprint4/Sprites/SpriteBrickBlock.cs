using Baritone.States.Powerups;
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
    public class SpriteBrickBlock : SpriteBlock
    {

        public SpriteBrickBlock(Sprint4 game) : base(game)
        {
            this.Info.frameDelay = 10;

            this.RegisterState(SpriteStates.Sheets.BRICK, SpriteStates.Sprites.IDLE, "Sprites/Blocks/brick");
            this.RegisterState(SpriteStates.Sheets.BROKEN, SpriteStates.Sprites.IDLE, "Sprites/Blocks/broken_brick");
            this.RegisterState(SpriteStates.Sheets.USED, SpriteStates.Sprites.IDLE, "Sprites/Blocks/used");
            this.SetSheetState(SpriteStates.Sheets.BRICK);
            this.SetSpriteState(SpriteStates.Sprites.IDLE); 
        }

        public override bool isStatic()
        {
            return true;
        }

        public override void Handle()
        {
            if (SpriteLayer.CollisionLayer.Sprites.Contains(this))
            {
                if (this.item == null && this.game.Mario.Info.spriteHeight > (16 * this.game.Mario.Info.scale))
                {
                    if (this.SheetState != SpriteStates.Sheets.USED)
                    {
                        Console.WriteLine("no item and mario isn't little. Breaking brick");
                        
                        SpriteBrokenBrick brokenBrick = new SpriteBrokenBrick(game,0);
                        SpriteBrokenBrick brokenBrick2 = new SpriteBrokenBrick(game,1);
                        SpriteBrokenBrick brokenBrick3 = new SpriteBrokenBrick(game,2);
                        SpriteBrokenBrick brokenBrick4 = new SpriteBrokenBrick(game,3);

                        SoundFactory.PlaySoundEffect(SoundFactory.BrickBreak());

                        brokenBrick.Info.position.X = this.Info.position.X;
                        brokenBrick.Info.position.Y = this.Info.position.Y;

                        brokenBrick2.Info.position.X = this.Info.position.X;
                        brokenBrick2.Info.position.Y = this.Info.position.Y;

                        brokenBrick3.Info.position.X = this.Info.position.X;
                        brokenBrick3.Info.position.Y = this.Info.position.Y;

                        brokenBrick4.Info.position.X = this.Info.position.X;
                        brokenBrick4.Info.position.Y = this.Info.position.Y;


                        SpriteLayer.CollisionLayer.AddSprite(brokenBrick);
                        SpriteLayer.CollisionLayer.AddSprite(brokenBrick2);
                        SpriteLayer.CollisionLayer.AddSprite(brokenBrick3);
                        SpriteLayer.CollisionLayer.AddSprite(brokenBrick4);

                        SpriteLayer.CollisionLayer.RemoveSprite(this);
                    }
                    else
                    {
                        this.Bump();
                    }
                }
                else
                {
                    this.Bump();
                    
                    Console.WriteLine("Either there's an item, or mario is little.");
                        if (this.item != null)
                        {
                            this.RevealItem();
                        }              
                }
            }
        }

    }
}
