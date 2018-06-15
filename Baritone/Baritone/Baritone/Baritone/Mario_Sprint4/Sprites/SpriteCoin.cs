using Baritone.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System.Threading.Tasks;

namespace Baritone.Sprites
{
    public class SpriteCoin : SpriteCollection, ICollidable, IRevealable
    {
        public SpriteCoin(Sprint4 game) : base(game){
            this.Info.frameDelay = 12;
            this.Info.harmfulIndex = -1;

            SpriteUtils.SetDefaultProperties(this, "Sprites/Items/coin");
        }

        public override bool isStatic()
        {
            return true;
        }

        public void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {
            if (other is SpriteMario)
            {
                SpriteMario mario = other as SpriteMario;
                if (SpriteLayer.CollisionLayer.Sprites.Contains(this))
                {
                    mario.coins++;
                    mario.points += 200;
                    this.game.pointGenerator.Add(200, this);

                    if (mario.coins / mario.nextLife == 100)
                    {
                        mario.lives++;
                        mario.nextLife++;
                    }

                    SoundFactory.PlaySoundEffect(SoundFactory.Coin());
                }
                SpriteLayer.CollisionLayer.RemoveSprite(this);
            }
        }

        public void Reveal()
        {
            int destY = (((int)(this.Info.position.Y)) - this.Info.spriteHeight) - 1;
            if (this.Info.bounds == null || this.Info.bounds == default(Rectangle))
            {
                this.Info.bounds = game.bounds;
            }
            this.Info.manual = true;
            this.Info.velocity.Y = -5;
            SpriteLayer.CollisionLayer.AddSprite(this);

            while (this.Info.position.Y > destY)
            {
                Console.WriteLine("Waiting for {0} to be revealed. Its velocity is {1}", this.name, this.Info.velocity.Y);
                Thread.Sleep(200);
            }
            Console.WriteLine("{0} has been revealed", this.name);

            this.Info.velocity.Y = 0;
            this.OnCollision(Direction.TOP, this.game.Mario, default(Rectangle));
            this.Info.manual = false;
        }
    }
}
