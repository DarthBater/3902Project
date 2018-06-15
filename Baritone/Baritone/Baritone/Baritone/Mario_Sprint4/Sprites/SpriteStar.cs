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

namespace Baritone.Sprites
{
    public class SpriteStar : SpriteCollection, IRevealable, ICollidable
    {

        /*
            Mushroom is a different sprite in that it is hidden behind a block before it is shown.
            it is added to the main sprite collection when you call StarAppear().

            if you want the star to be instantly visible, add it to the sprite collection
            as normal. It will be still if you do this unless you call Move();
        */

        public SpriteStar(Sprint4 game) : base(game)
        {
            this.Info.layer = 1;

            this.RegisterState(SpriteStates.Sheets.NORMAL, SpriteStates.Sprites.IDLE, "Sprites/Items/star");
            this.SetSheetState(SpriteStates.Sheets.NORMAL);
            this.SetSpriteState(SpriteStates.Sprites.IDLE);
            this.Info.bounce = true;
        }

        public override bool isStatic()
        {
            return false;
        }

        public void Move()
        {
            if (this.game.Mario.Info.position.X > this.Info.position.X)
            {
                this.Info.velocity.X = -1;
            } else
            {
                this.Info.velocity.X = 1;
            }
        }

        public void OnCollision(Direction direction, SpriteCollection other, Rectangle intersection)
        {
            if (other is SpriteMario)
            {
                SpriteLayer.CollisionLayer.RemoveSprite(this);

                (other as SpriteMario).StateMachinePowerup.CurrentState.ReceiveStar();
                (other as SpriteMario).points += 1000;
                this.game.pointGenerator.Add(1000, this.game.Mario);
            }
        }

        public void Reveal()
        {
            if (!SpriteLayer.CollisionLayer.Sprites.Contains(this))
            {
                Task<bool> t = SpriteUtils.RevealItem(this.game, this);
                Task.Factory.StartNew( delegate() { t.Wait(); this.Move(); } );
            }
        }

    }
}
