using Baritone.Utils;
using Baritone.Wrappers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Baritone.Sprites
{
    public class SpriteBrokenBrick : SpriteCollection, ITrackable
    {
        public SpriteBrokenBrick(Sprint4 game, int frame) : base(game)
        {
            this.Info.frameDelay = 10;
            this.Info.bounds = game.bounds;
            this.Info.spriteWidth = 8;
            this.Info.currentFrame = frame;
            this.Info.bounds = game.bounds;

            SpriteUtils.SetDefaultProperties(this, "Sprites/Blocks/broken_brick2");

            switch (frame)
            {
                case 0: this.Info.velocity.X = -1;
                    break;
                case 1: this.Info.velocity.X = 1;
                    break;
                case 2: this.Info.velocity.X = 1;
                    this.Info.velocity.Y = -2;
                    break;
                case 3: this.Info.velocity.X = -1;
                    this.Info.velocity.Y = -2;
                    break;
                default:
                    this.Info.velocity.X = 1;
                    break;
            }


        }

        public override bool isStatic()
        {
            return false;
        }

        public void OnMove(int x, int y)
        {
            if (y > (this.game.bounds.Bottom - this.Info.spriteHeight - 1))
            {
                Task.Factory.StartNew(delegate () { SpriteLayer.CollisionLayer.RemoveSprite(this); });
            }
        }
    }
}
