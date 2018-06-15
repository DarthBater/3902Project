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
    public class SpriteFireFlower : SpriteCollection, IRevealable, ICollidable
    {

        /*
            Mushroom is a different sprite in that it is hidden behind a block before it is shown.
            it is added to the main sprite collection when you call MushroomAppear().

            if you want the mushroom to be instantly visible, add it to the sprite collection
            as normal. It will be still if you do this unless you call Move();
        */
        public SpriteFireFlower(Sprint4 game) : base(game)
        {
            this.Info.layer = 0.5f;
            this.Info.harmfulIndex = -1;

            SpriteUtils.SetDefaultProperties(this, "Sprites/Items/fireflower");
        }

        public override bool isStatic()
        {
            return false;
        }

        public void Move()
        {
            this.Info.velocity.Y = 0;
            this.Info.velocity.X = 0;
            this.Info.bounce = true;
        }

        public void Reveal()
        {

            if (!SpriteLayer.CollisionLayer.Sprites.Contains(this))
            {
                SpriteUtils.RevealItem(this.game, this);
            }
        }

        public void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {
            if (other is SpriteMario)
            {
                SpriteMario mario = other as SpriteMario;
                SpriteLayer.CollisionLayer.RemoveSprite(this);

                mario.StateMachinePowerup.CurrentState.ReceiveFireFlower();
                mario.points += 1000;
                this.game.pointGenerator.Add(1000, this);
            }
        }
    }
}
