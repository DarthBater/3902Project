using Baritone.Utils;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Baritone.Wrappers;
using System.Threading;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace Baritone.Sprites
{
    public class SpriteMushroom : SpriteCollection, IRevealable, ICollidable
    {

        private bool oneUp = false;
        /*
            Mushroom is a different sprite in that it is hidden behind a block before it is shown.
            it is added to the main sprite collection when you call MushroomAppear().

            if you want the mushroom to be instantly visible, add it to the sprite collection
            as normal. It will be still if you do this unless you call Move();
        */
        public SpriteMushroom(Sprint4 game, String path) : base(game)
        {
            this.Info.layer = 0.5f;
            this.Info.harmfulIndex = -1;

            if (path.Contains("oneupshroom"))
            {
                this.oneUp = true;
            }

            SpriteUtils.SetDefaultProperties(this, path);
            this.Info.bounce = true;
        }

        public override bool isStatic()
        {
            return false;
        }

        public void Move()
        {
            if (this.game.Mario.Info.position.X < this.Info.position.X)
            {
                if (this.oneUp)
                {
                    this.Info.velocity.X = 1;
                } else
                {
                    this.Info.velocity.X = -1;
                }
            } else
            {
                if (this.oneUp)
                {
                    this.Info.velocity.X = -1;
                } else
                {
                    this.Info.velocity.X = 1;
                }
            }
        }

        public void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {
            if (other is SpriteMario)
            {
                SpriteMario mario = other as SpriteMario;
                SpriteLayer.CollisionLayer.RemoveSprite(this);
                if (this.oneUp)
                {
                    mario.lives++;
                    SoundFactory.PlaySoundEffect(SoundFactory.OneUp());
                    
                }
                else
                {
                    mario.StateMachinePowerup.CurrentState.ReceiveMushroom();
                    mario.points += 1000;
                    this.game.pointGenerator.Add(1000, mario);
                }                
            }
        }

        public void Reveal()
        {
            if (!SpriteLayer.CollisionLayer.Sprites.Contains(this))
            {
                Task<bool> t = SpriteUtils.RevealItem(this.game, this);
                Task.Factory.StartNew(delegate () { t.Wait(); this.Move(); });
            }
        }
    }
}
