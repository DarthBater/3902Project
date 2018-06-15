using Baritone.Utils;
using Baritone.Wrappers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baritone.Sprites
{
    class SpriteFireball : SpriteCollection, ICollidable, ITrackable
    {

        public SpriteFireball(Sprint4 game) : base(game)
        {
            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.IDLE, "Sprites/Obstacles/fireball");
        }

        public override bool isStatic()
        {
            return false;
        }

        public void OnCollision(Direction d, SpriteCollection other, Rectangle intersection)
        {
            if (other is SpriteFloor)
            {
                this.Info.velocity.Y = -(this.Info.velocity.Y / 2);
            }
            else if (other is SpriteStair)
            {
                SpriteLayer.CollisionLayer.RemoveSprite(this);
            }
            else if (other is SpriteGoomba)
            {
                (other as SpriteGoomba).Die();
                this.game.Mario.points += 100;
                SpriteLayer.CollisionLayer.RemoveSprite(this);
            }
            /*else if (other is SpriteKoopa)
            {
                (other as SpriteKoopa).Die();
                this.game.Mario.points += 100;
                SpriteLayer.CollisionLayer.RemoveSprite(this);
            }//*/
        }

        public void OnMove(int x, int y)
        {
            Rectangle viewport = this.game.GetViewport();

            if (x - 2 <= viewport.Left || x + this.Info.spriteWidth + 2 >= viewport.Right
                || y - 2 <= viewport.Top || y + this.Info.spriteHeight + 2 >= viewport.Bottom
                || this.Info.velocity.X == 0)
            {
                //Offload the removal because OnMove is called within Update,
                //which is already being locked.
                Task.Factory.StartNew(delegate () {
                    lock(SpriteLayer.CollisionLayer)
                    {
                        SpriteLayer.CollisionLayer.Sprites.Remove(this);
                    }
                });
            }
        }
    }
}
